using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ARDrone.Capture
{
    public class SnapshotRecorder
    {
        public SnapshotRecorder()
        {

        }

        public void SaveSnapshot(Bitmap image, String filePath)
        {
            image.Save(filePath);
            image.Dispose();
        }

    }
}
