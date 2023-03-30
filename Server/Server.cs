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
using System.Diagnostics;
using System.CodeDom;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using System.Printing.IndexedProperties;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Markup;

namespace Server
{
    public class ProgramServer
    {
        //Store the current state of the server - by default it should be idle
        Server.states currentState = states.Idle;
        String currentClientUsername = "Server";
        private userLoginData userData;
        private string currentOriginalImage = "NoImagePlaceHolder.png";
        private string currentAnalyzedImage = "NoImagePlaceHolder.png";

        //This will act as the servers "main" and any/all connection to client, loading can be done here
        public void run()
        {
            Packet packet = new Packet();
            Int32 port = 11000;
            TcpListener server = new TcpListener(IPAddress.Loopback, port);
            server.Start();
            byte[] buffer = new byte[1026];

            using TcpClient client = server.AcceptTcpClient();
            bool connectedUser = false;
            NetworkStream stream = client.GetStream();

            int i;

            while (!connectedUser)
            {
                // Loop to receive all the data sent by the client.
                i = stream.Read(buffer, 0, buffer.Length);
                byte[] data = new byte[i];
                Array.Copy(buffer, data, i);

                Packet recvPacket = new Packet(data);

                IntializeUserData(recvPacket);



                if (recvPacket.GetHead().getState() == states.Auth)
                {
                    //if we are authentcating a user
                    currentState = states.Auth;

                    string message = SignInUser("users.txt");

                    if (message == "User signed in")
                    {

                        //sets up a packet with nothing just to say that it was recived and passed
                        packet.setHead('2', '1', states.Recv);

                        packet.SerializeData();
                        byte[] sendbuf = packet.getTailBuffer();
                        NetworkStream clStream = client.GetStream();

                        clStream.Write(sendbuf, 0, sendbuf.Length);
                        connectedUser = true;
                    }
                    else
                    {

                        //sends it back if it failed and needs to be reauthed.
                        packet.setHead('2', '1', states.Auth);

                        packet.SerializeData();

                        byte[] sendbuf = packet.getTailBuffer();
                        NetworkStream clStream = client.GetStream();

                        clStream.Write(sendbuf, 0, sendbuf.Length);

                    }
                }
                else if (recvPacket.GetHead().getState() == states.NewAuth)
                {
                    //If the user is wanting to be registered
                    currentState = states.NewAuth;

                    string message = RegisterUser("users.txt");

                    if (message == "User registered")
                    {

                        //sets up a packet with nothing just to say that it was recived and passed
                        packet.setHead('2', '1', states.Recv);

                        packet.SerializeData();
                        byte[] sendbuf = packet.getTailBuffer();
                        NetworkStream clStream = client.GetStream();

                        clStream.Write(sendbuf, 0, sendbuf.Length);


                        connectedUser = true;

                    }
                    else
                    {

                        //sends it back if it failed and needs to be reauthed.
                        packet.setHead('2', '1', states.Auth);

                        packet.SerializeData();

                        byte[] sendbuf = packet.getTailBuffer();
                        NetworkStream clStream = client.GetStream();

                        clStream.Write(sendbuf, 0, sendbuf.Length);

                    }
                }
            }




        }


        public void SocketCleanup(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void setCurrentOriginalImage(string originalImage)
        {
            currentOriginalImage = originalImage;
        }

        public void setCurrentAnalyzedImage(string analyzedImage)
        {
            currentAnalyzedImage = analyzedImage;
        }

        public string getCurrentOriginalImage()
        {
            return currentOriginalImage;
        }

        public string getCurrentAnalyzedImage()
        {
            return currentAnalyzedImage;
        }
      

        public ProgramServer GetProgramServer()
        {
            return this;
        }

        public string[,] RunRecognition(string filename)
        {
            setAnalyzingImagesState();

            var assetsRelativePath = @"../../../MLNET/assets";
            string assetsPath = GetAbsolutePath(assetsRelativePath);
            var modelFilePath = Path.Combine(assetsPath, "Model", "TinyYolo2_model.onnx");
            var imagesFolder = Path.Combine(assetsPath, "images");
            var outputFolder = Path.Combine(assetsPath, "images", "output");
            var image = Path.Combine(imagesFolder, filename);
            string imagefileName = filename;
            string imageFilePath = Path.Combine(assetsPath, "images", imagefileName);

            // Initialize MLContext
            MLContext mlContext = new MLContext();

            try
            {
                // Load the image data
                var imageData = new ImageNetData() { ImagePath = imageFilePath };
                var imageDataView = mlContext.Data.LoadFromEnumerable(new[] { imageData });
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

                string[,] results = new string[1, 2];

                // Draw bounding boxes for detected objects in the image
                string imageFileName = filename;
                IList<YoloBoundingBox> detectedObjects = boundingBoxes.First();

                DrawBoundingBox(imagesFolder, outputFolder, imageFileName, detectedObjects);
                string[] objects = detectedObjects.Select(obj => obj.Label).ToArray();
                string[] confidence = detectedObjects.Select(obj => obj.Confidence.ToString()).ToArray();

                LogDetectedObjects(imageFileName, detectedObjects);

                for (int j = 0; j < objects.Length; j++)
                {
                    results[0, j] = objects[j];
                    results[0, j + 1] = confidence[j];
                }

                Console.WriteLine("========= End of Process..Hit any Key ========");
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing file {0}: {1}", filename, ex.Message);
                return null;
            }



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

        public void saveUserSpecificEventToFile(string eventToLog)
        {
            //log for a specific user
            string path = currentClientUsername + "Log.txt";
            string logEntry = "Username: " + currentClientUsername + eventToLog + ": Time of day " + DateTime.Now;

            if (!File.Exists(path))
            { // Create a file to write to
                using (StreamWriter Writer = new StreamWriter(File.Create(path)))
                {
                    Writer.WriteLine(logEntry);
                    Writer.Close();
                }
            }
            else
            { //write to general program file
                using (StreamWriter writer = new StreamWriter("ServerLog.txt", append: true))
                {
                    writer.WriteLine(logEntry);
                    writer.Close();
                }
            }
        }

        public void saveServerEventToFile(string eventToLog)
        {
            //log for a specific user
            string path = currentClientUsername + "Log.txt";
            string logEntry = "Username: " + currentClientUsername + eventToLog + ": Time of day " + DateTime.Now;

            //write to general program file
            using (StreamWriter writer = new StreamWriter("ServerLog.txt", append: true))
            {
                writer.WriteLine(logEntry);
                writer.Close();
            }
        }

        public string openFile(String filename)
        {
            string readText = File.ReadAllText(filename);

            return readText;
        }

        //NOTE: For the following states, if a state has a automatic trigger the state can be trigged by the server reaching a certain point in the code. Otherwise the state can only be triggered from the received client packet

        //State Command: 1 – Idle (Automatic Trigger)​​
        public void setIdleState()
        {
            //Server is waiting for next state​​
            currentState = states.Idle;
            saveUserSpecificEventToFile(" state changed to idle");
            saveServerEventToFile(" state changed to idle");
        }

        //State Command: 2 - Authenticating(Client Header Trigger)​​
        public void setAutenticatingState()
        {
            //Server is determining if the client can be accepted​​
            currentState = states.Auth;
            saveUserSpecificEventToFile(" state changed to authenticating");
            saveServerEventToFile(" state changed to authenticating");
        }

        //State Command: 3 - Receiving Packets(Client Header Trigger)​​
        public void setReceivingPacketsState()
        {
            //Server is getting packets​​
            currentState = states.Recv;
            saveUserSpecificEventToFile(" state changed to receiving packets");
            saveServerEventToFile(" state changed to receiving packets");
        }

        //State Command: 4 - Analyzing Images(Automatic Trigger)​​
        public void setAnalyzingImagesState()
        {
            //Server is classifying the given image​​
            currentState = states.Analyze;
            saveUserSpecificEventToFile(" state changed to analyzing images");
            saveServerEventToFile(" state changed to analyzing images");
        }

        //State Command: 5 - Saving Images(Automatic Trigger)​​
        public void setSavingImagesState()
        {
            //Server is internally saving the image and preparing to send the image​​
            currentState = states.Saving;
            saveUserSpecificEventToFile(" state changed to saving images");
            saveServerEventToFile(" state changed to saving images");
        }

        //State Command: 6 - Sending Analyzed Images(Client Header Trigger)​
        public void setSendingAnalyzedImagesState()
        {
            //Server is sending the image packet​
            currentState = states.Sending;
            saveUserSpecificEventToFile(" state changed to sending analyzed images");
            saveServerEventToFile(" state changed to sending analyzed images");
        }
        public states getCurrentState()
        {
            return currentState;
        }

        public void IntializeUserData(Packet packet)
        {
            userLoginData temp = packet.deserializeUserLoginData();
            userData.setUserName(temp.getUserName());
            userData.setPassword(temp.getPassword());
        }

        public userLoginData GetuserData()
        {
            return userData;
        }

        public void SetuserData(string username, string password)
        {
            userData.setUserName(username);
            userData.setPassword(password);
        }

        public bool SaveuserData(string filePath)
        {

            bool error = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(userData.getUserName());
                    writer.WriteLine(userData.getPassword());
                    writer.Close();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
                error = true;
            }

            return error;
        }

        public string SignInUser(string filePath)
        {


            string message = "Username or password is incorrect";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line1 = reader.ReadLine();
                    string line2 = reader.ReadLine();

                    while ((line1 != null) && (line2 != null))
                    {
                        if ((line1 == userData.getUserName()) && (line2 == userData.getPassword()))
                        {

                            message = "User signed in";
                            break;
                        }

                        line1 = reader.ReadLine();
                        line2 = reader.ReadLine();
                    }



                    reader.Close();
                }
            }
            catch (IOException e)
            {
                message = "Error Signing in user";
            }

            return message;
        }


        public bool checkUsername(string filePath)
        {
            bool unique = true;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line1 = reader.ReadLine();

                    while ((line1 != null))
                    {
                        if ((line1 == userData.getUserName()))
                        {
                            unique = false;
                            break;
                        }

                        line1 = reader.ReadLine();
                    }



                    reader.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
            }

            return unique;
        }


        public string RegisterUser(string filePath)
        {
            string message;
            if (checkUsername(filePath) == false)
            {
                //Non unique Username Entered
                message = "Username must be unique";
            }
            else
            {
                if (SaveuserData(filePath) == false)
                {
                    message = "User registered";
                }
                else
                {
                    message = "Error Saving user's credentials";
                }
            }

            return message;
        }
    }
}