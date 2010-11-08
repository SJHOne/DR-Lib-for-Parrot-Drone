using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using AviFile;

namespace ARDrone.Video
{
    public class VideoRecorder
    {
        private AviManager videoManager = null;
        private VideoStream stream = null;

        private bool isVideoCaptureStarted = false;
        private bool isCompressing = false;

        private String uncompressedFileName;
        private String finalFileName;

        private bool compress = false;

        private Thread compressThread = null;

        public event ErrorEventHandler CompressionError;
        public event EventHandler CompressionComplete;

        public VideoRecorder()
        {

        }

        public void Dispose()
        {
            CompressionError = null;
            CompressionComplete = null;

            if (isVideoCaptureStarted)
            {
                videoManager.Close();
            }

            if (isCompressing)
            {
                if (compressThread != null)
                {
                    try
                    {
                        compressThread.Abort();
                    }
                    catch (Exception) { }
                }
            }


        }

        public void StartVideo(String filePath, int frameRate, int width, int height, System.Drawing.Imaging.PixelFormat pixelFormat, int bytesPerPixel, bool compressed)
        {
            if (isVideoCaptureStarted || isCompressing)
            {
                throw new Exception("Another video is currently being recorded or processed");
            }

            compressThread = null;
            compress = compressed;
            if (!compress)
            {
                finalFileName = uncompressedFileName = filePath;
            }
            else
            {
                String directory = Path.GetDirectoryName(filePath);
                String fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                String extension = Path.GetExtension(filePath);

                uncompressedFileName = Path.Combine(directory, fileNameWithoutExtension + "_uncompressed." + extension);
                finalFileName = filePath;
            }
            
            isVideoCaptureStarted = true;
            isCompressing = false;

            videoManager = new AviManager(uncompressedFileName, false);
            stream = videoManager.AddVideoStream(false, frameRate, width * height * bytesPerPixel, width, height, pixelFormat);
        }

        public void AddFrame(Bitmap image)
        {
            if (stream != null)
            {
                stream.AddFrame(image);
            }
        }

        public void EndVideo()
        {
            if (isVideoCaptureStarted)
            {
                videoManager.Close();
                if (compress)
                {
                    Compress();
                }

                isVideoCaptureStarted = false;

                compress = false;
                stream = null;
                videoManager = null;
            }
        }

        public void Compress()
        {
            isCompressing = true;
            compressThread = new Thread(new ThreadStart(DoCompressionWork));
            compressThread.Start();
        }

        public void DoCompressionWork()
        {
            AviManager videoManager = new AviManager(uncompressedFileName, true);
            VideoStream stream = videoManager.GetVideoStream();

            bool error = false;
            String errorMessage = "";

            VideoStream newStream = null;
            AviManager newManager = null;

            try
            {
                newManager = stream.DecompressToNewFile(finalFileName, true, out newStream);
            }
            catch (Exception e)
            {
                error = true;
                errorMessage = e.Message;
            }
            finally
            {
                try { videoManager.Close(); }
                catch (Exception) { }

                try { newManager.Close(); }
                catch (Exception) { }
            }

            if (!error)
            {
                File.Delete(uncompressedFileName);
                if (CompressionError != null)
                {
                    CompressionComplete.Invoke(this, null);
                }
            }
            else
            {
                try
                {
                    File.Delete(finalFileName);
                }
                catch (Exception) { }

                if (CompressionError != null)
                {
                    CompressionError.Invoke(this, new ErrorEventArgs(new Exception("Error during compresion: " + errorMessage)));
                }
            }

            isCompressing = false;
        }

        public bool IsVideoCaptureStarted  { get { return isVideoCaptureStarted; } }
        public bool IsVideoCaptureEnded { get { return !isVideoCaptureStarted && !isCompressing; } }
    }
}
