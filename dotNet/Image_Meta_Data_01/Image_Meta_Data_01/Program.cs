using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace Image_Meta_Data_01
{

    /// <summary>
    /// <References> https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.propertyitem.id?view=netframework-4.8 </References>
    /// <References> https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions </References>
    /// <References> http://csharphelper.com/blog/2016/07/list-images-exif-properties-c/ </References>
    /// <References> https://nicholasarmstrong.com/2010/02/exif-quick-reference/ </References>
    /// <References> https://github.com/anton-iermolenko/Photo-library-toolkit/blob/master/PhotoLibaryToolkit/Framework/ImageConstants.cs </References>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // GetImageFileFromResources();

            Console.WriteLine("-------------");

            // GetImageFile();

            GetGIFFrames(@"C:\Users\cportelli\Documents\Professional\Konstru Images\image10.gif");

            Console.ReadKey();
        }

        /// <summary>
        /// Get the frames from an animated GIF
        /// </summary>
        /// <param name="FilePath">Full file path of the GIF</param>
        public static void GetGIFFrames(string FilePath)
        {
            // exit if file doesn't exist
            if (!System.IO.File.Exists(FilePath))
            {
                Console.WriteLine("File does not exist: " + FilePath);
                return;
            }

            string Path = System.IO.Path.GetDirectoryName(FilePath);                            // get file path
            string FileName = System.IO.Path.GetFileName(FilePath);                             // get file name
            string FileExtension = System.IO.Path.GetExtension(FilePath);                       // get file extension

            if (FileName.Contains(FileExtension))
                FileName = FileName.TrimEnd(FileExtension.ToArray());                           // remove the extension

            string NewFolderPath = Path + @"\" + FileName;

            System.IO.DirectoryInfo newDirectory = System.IO.Directory.CreateDirectory(NewFolderPath);
            if (!System.IO.Directory.Exists(NewFolderPath))
            {
                Console.WriteLine("Cannot create new folder path");
                return;
            }

            Image img = Image.FromFile(FilePath);                                               // load image
            int frames = img.GetFrameCount(FrameDimension.Time);                                // get number of frames
            byte[] times = img.GetPropertyItem(0x5100).Value;
            string suffix;

            for (int i = 0; i < frames; i++)
            {
                suffix = i.ToString();
                int dur = BitConverter.ToInt32(times, 4 * i);                                   // get frame duration
                img.SelectActiveFrame(FrameDimension.Time, i);                                  // set active frame
                Image frame = new Bitmap(img);                                                  // get frame

                if (i < 10)                                                                     // set frame counter
                    suffix = "00" + i.ToString();
                else if (i < 100)
                    suffix = "0" + i.ToString();

                frame.Save(NewFolderPath + @"\Frame_" + suffix + ".jpg");                                // save frame
            }
        }

        public static void GetImageFileFromResources()
        {
            Image img = new Bitmap(Properties.Resources.IMG_8058);          // get image file from resources
            PropertyItem[] properties = img.PropertyItems;                  // extract properties

            Console.WriteLine("Number of properties: " + properties.Length);

            foreach (PropertyItem propItem in properties)
                Console.WriteLine($"{propItem.Id} : {propItem.Value}");     // print properties
        }


        public static void GetImageFile()
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            string filePath = @"C:\Users\cportelli\Documents\Personal\GitHub\Coding_Sketchbook\dotNet\Image_Meta_Data_01\Image_Meta_Data_01\Images\IMG_8058.JPG";
            Image img = new Bitmap(filePath);                               // get image file from resources
            PropertyItem[] properties = img.PropertyItems;                  // extract properties

            Console.WriteLine("Number of properties: " + properties.Length);

            foreach (PropertyItem propItem in properties)
            {
                ExifPropertyData data = new ExifPropertyData();
                data.Id = propItem.Id;
                data.PropertyType = propItem.Type.GetType();
                data.DataBuffer = propItem.Value;
                data.DataType = (ExifPropertyDataTypes)propItem.Type;
                data.DataLength = propItem.Len;

                string result = "";
                int num_items, item_size;

                switch (data.DataType)
                {
                    case ExifPropertyDataTypes.ByteArray:
                    case ExifPropertyDataTypes.UByteArray:
                        data.DataString = BitConverter.ToString(data.DataBuffer);
                        break;

                    case ExifPropertyDataTypes.String:
                        data.DataString = Encoding.UTF8.GetString(data.DataBuffer, 0, data.DataLength - 1);
                        break;

                    case ExifPropertyDataTypes.UShortArray:
                        result = "";
                        item_size = 2;
                        num_items = data.DataLength / item_size;
                        for (int i = 0; i < num_items; i++)
                        {
                            ushort value = BitConverter.ToUInt16(data.DataBuffer, i * item_size);
                            result += ", " + value.ToString();
                        }
                        if (result.Length > 0) result = result.Substring(2);
                        data.DataString = "[" + result + "]";
                        break;

                    case ExifPropertyDataTypes.ULongArray:
                        result = "";
                        item_size = 4;
                        num_items = data.DataLength / item_size;
                        for (int i = 0; i < num_items; i++)
                        {
                            uint value = BitConverter.ToUInt32(data.DataBuffer, i * item_size);
                            result += ", " + value.ToString();
                        }
                        if (result.Length > 0) result = result.Substring(2);
                        data.DataString = "[" + result + "]";
                        break;

                    case ExifPropertyDataTypes.ULongFractionArray:
                        result = "";
                        item_size = 8;
                        num_items = data.DataLength / item_size;
                        for (int i = 0; i < num_items; i++)
                        {
                            uint numerator = BitConverter.ToUInt32(data.DataBuffer, i * item_size);
                            uint denominator = BitConverter.ToUInt32(data.DataBuffer, i * item_size + item_size / 2);
                            result += ", " + numerator.ToString() + "/" + denominator.ToString();
                        }
                        if (result.Length > 0) result = result.Substring(2);
                        data.DataString = "[" + result + "]";
                        break;

                    case ExifPropertyDataTypes.LongArray:
                        result = "";
                        item_size = 4;
                        num_items = data.DataLength / item_size;
                        for (int i = 0; i < num_items; i++)
                        {
                            int value = BitConverter.ToInt32(data.DataBuffer, i * item_size);
                            result += ", " + value.ToString();
                        }
                        if (result.Length > 0) result = result.Substring(2);
                        data.DataString = "[" + result + "]";
                        break;

                    case ExifPropertyDataTypes.LongFractionArray:
                        result = "";
                        item_size = 8;
                        num_items = data.DataLength / item_size;
                        for (int i = 0; i < num_items; i++)
                        {
                            int numerator = BitConverter.ToInt32(data.DataBuffer, i * item_size);
                            int denominator = BitConverter.ToInt32(data.DataBuffer, i * item_size + item_size / 2);
                            result += ", " + numerator.ToString() + "/" + denominator.ToString();
                        }
                        if (result.Length > 0) result = result.Substring(2);
                        data.DataString = "[" + result + "]";
                        break;
                }

                //string val = encoding.GetString(propItem.Value);

                Console.WriteLine($"{propItem.Id} : {propItem.Type} : {data.DataString}");     // print properties

            }


        }
    }

    public struct ExifPropertyData
    {
        public int Id;
        //public ExifPropertyTypes PropertyType;
        public Type PropertyType;
        public ExifPropertyDataTypes DataType;
        public byte[] DataBuffer;
        public int DataLength;
        public string DataString;
    }

    public enum ExifPropertyDataTypes : short
    {
        ByteArray = 1,
        String = 2,
        UShortArray = 3,
        ULongArray = 4,
        ULongFractionArray = 5,
        UByteArray = 6,
        LongArray = 7,
        LongFractionArray = 10,
    }
}
