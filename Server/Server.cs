using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using ObjectDetection.DataStructures;
using Microsoft.ML;
using System.IO;
using Server.ML.NET.YoloParser;
using Server.ML.NET;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;
using FontStyle = System.Drawing.FontStyle;

namespace Server
{
    public class ProgramServer
    {
        //Store the current state of the server - by default it should be idle
        Server.states currentState = states.Idle;

        public void RunRecognition()
        {
            var assetsRelativePath = @"../../../ML.NET/assets";
            string assetsPath = GetAbsolutePath(assetsRelativePath);
            var modelFilePath = Path.Combine(assetsPath, "Model", "TinyYolo2_model.onnx");
            var imagesFolder = Path.Combine(assetsPath, "images");
            var outputFolder = Path.Combine(assetsPath, "images", "output");

            // Initialize MLContext
            MLContext mlContext = new MLContext();

            try
            {
                // Load Data
                IEnumerable<ImageNetData> images = ImageNetData.ReadFromFile(imagesFolder);
                IDataView imageDataView = mlContext.Data.LoadFromEnumerable(images);

                // Create instance of model scorer
                var modelScorer = new OnnxModelScorer(imagesFolder, modelFilePath, mlContext);

                // Use model to score data
                IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

                // Post-process model output
                YoloOutputParser parser = new YoloOutputParser();

                var boundingBoxes =
                    probabilities
                    .Select(probability => parser.ParseOutputs(probability))
                    .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

                // Draw bounding boxes for detected objects in each of the images
                for (var i = 0; i < images.Count(); i++)
                {
                    string imageFileName = images.ElementAt(i).Label;
                    IList<YoloBoundingBox> detectedObjects = boundingBoxes.ElementAt(i);

                    DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);

                    LogDetectedObjects(imageFileName, detectedObjects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("========= End of Process..Hit any Key ========");

            string GetAbsolutePath(string relativePath)
            {
                FileInfo _dataRoot = new FileInfo(typeof(model).Assembly.Location);
                string assemblyFolderPath = _dataRoot.Directory.FullName;

                string fullPath = Path.Combine(assemblyFolderPath, relativePath);

                return fullPath;
            }

            void DrawBoundingBox(string inputImageLocation, string outputImageLocation, string imageName, IList<YoloBoundingBox> filteredBoundingBoxes)
            {
                Image image = Image.FromFile(Path.Combine(inputImageLocation, imageName));

                var originalImageHeight = image.Height;
                var originalImageWidth = image.Width;

                foreach (var box in filteredBoundingBoxes)
                {
                    // Get Bounding Box Dimensions
                    var x = (uint)Math.Max(box.Dimensions.X, 0);
                    var y = (uint)Math.Max(box.Dimensions.Y, 0);
                    var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                    var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                    // Resize To Image
                    x = (uint)originalImageWidth * x / OnnxModelScorer.ImageNetSettings.imageWidth;
                    y = (uint)originalImageHeight * y / OnnxModelScorer.ImageNetSettings.imageHeight;
                    width = (uint)originalImageWidth * width / OnnxModelScorer.ImageNetSettings.imageWidth;
                    height = (uint)originalImageHeight * height / OnnxModelScorer.ImageNetSettings.imageHeight;

                    // Bounding Box Text
                    string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

                    using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                    {
                        thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        // Define Text Options
                        Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                        SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                        SolidBrush fontBrush = new SolidBrush(Color.Black);
                        Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                        // Define BoundingBox options
                        Pen pen = new Pen(box.BoxColor, 3.2f);
                        SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                        // Draw text on image 
                        #pragma warning disable CA1416 // Validate platform compatibility
                        thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                        #pragma warning restore CA1416 // Validate platform compatibility
                        thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);

                        // Draw bounding box on image
                        thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                    }
                }

                if (!Directory.Exists(outputImageLocation))
                {
                    Directory.CreateDirectory(outputImageLocation);
                }

                image.Save(Path.Combine(outputImageLocation, imageName));
            }

            void LogDetectedObjects(string imageName, IList<YoloBoundingBox> boundingBoxes)
            {
                Console.WriteLine($".....The objects in the image {imageName} are detected as below....");

                foreach (var box in boundingBoxes)
                {
                    Console.WriteLine($"{box.Label} and its Confidence score: {box.Confidence}");
                }

                Console.WriteLine("");
            }
        }


        //NOTE: For the following states, if a state has a automatic trigger the state can be trigged by the server reaching a certain point in the code. Otherwise the state can only be triggered from the received client packet

        //State Command: 1 – Idle (Automatic Trigger)​​
        void setIdleState()
        {
            //Server is waiting for next state​​
            currentState = states.Idle;
        }

        //State Command: 2 - Authenticating(Client Header Trigger)​​
        void setAutenticatingState()
        {
            //Server is determining if the client can be accepted​​
            currentState = states.Auth;
        }

        //State Command: 3 - Receiving Packets(Client Header Trigger)​​
        void setReceivingPacketsState()
        {
            //Server is getting packets​​
            currentState = states.Recv;
        }

        //State Command: 4 - Analyzing Images(Automatic Trigger)​​
        void setAnalyzingImagesState()
        {
            //Server is classifying the given image​​
            currentState = states.Analyze;
        }

        //State Command: 5 - Saving Images(Automatic Trigger)​​
        void setSavingImagesState()
        {
            //Server is internally saving the image and preparing to send the image​​
            currentState = states.Saving;
        }

        //State Command: 6 - Sending Analyzed Images(Client Header Trigger)​
        void setSendingAnalyzedImagesState()
        {
            //Server is sending the image packet​
            currentState = states.Sending;
        }
    }
}