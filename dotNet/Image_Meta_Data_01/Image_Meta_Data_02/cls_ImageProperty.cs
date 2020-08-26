using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Meta_Data_02
{
    public class cls_ImageProperty
    {
        #region ---- PROPERTIES ----

        /// <summary>
        /// Data table row number
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// ID value
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// Property Type
        /// </summary>
        public Type PropertyType { get; set; }

        /// <summary>
        /// EXIF Data type
        /// </summary>
        public ExifPropertyDataTypes DataType { get; set; }

        /// <summary>
        /// Data Length
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// Data Buffer , Raw Byte Array
        /// </summary>
        public byte[] DataBuffer { get; set; }

        private Dictionary<int, string> PropertyTags { get; set; }

        private Dictionary<int, string> PropertyResUnit { get; set; }

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public cls_ImageProperty()
        {
            CreatePropertyTags();
        }


        #region ---- METHODS ----

        /// <summary>
        /// Calculate value by converting bytes to string and find the value
        /// </summary>
        public void CalculateValue()
        {
            string result = FindDataValue();                                                    // convert bytes to sring

            if (PropertyTags.ContainsKey(Id))
                Property = PropertyTags[Id];
            else
                Property = Id.ToString();

            switch (Id)                                                                         // some special conversions are necessary
            {
                case 0x0112: Value = FindImageRotation(result); break;                          // image rotation
                case 0x9207: Value = FindMeteringMode(result); break;                           // metering mode
                case 0x9101: Value = FindCompressionConfiguration(); break;                     // components configuration
                case 0x9209: Value = FindFlashValue(result); break;                             // flash
                case 0x927c: Value = FindMakerNotes(result); break;
                case 0xa001: Value = FindColorSpace(result); break;
                case 0xa217: Value = FindSensingMethod(result); break;
                case 0xa301: Value = DataBuffer[0] == 1 ? "Directly Photographed" : "Unknown"; break;
                case 0xa402: Value = FindExposureMode(result); break;
                case 0xa403: Value = Convert.ToInt32(result) == 0 ? "Auto" : "Manual"; break;
                case 0xa406: Value = FindSceneCaptureType(result); break;
                case 0x000C: Value = FindGPSSpeedReference(result); break;
                case 0x8822: Value = FindExposureProgram(result); break;
                case 0x829A: Value = result += " seconds"; break;
                case 0x9000: Value = Encoding.UTF8.GetString(DataBuffer); break;
                case 0x8830: Value = FindSensitivityType(result); break;
                case 0xA401: Value = FindCustomRendered(result); break;
                case 0x0132:                                                                    // modify date
                case 0x9003:                                                                    // original date time
                case 0x9004: Value = FindDateTime(result); break;                               // Created date time
                case 0x0010:
                case 0x0017: Value = result == "T" ? "True North" : result == "M" ? "Magnetic North" : result; break;   // GPS North direction
                case 0x5030:
                case 0xA210:
                case 0x0128:                                                                    // resolution units
                    int val = Convert.ToInt32(result);
                    Value = PropertyResUnit[val]; break;
                case 0x0213:                                                                    // Y Cb Cr Positioning
                    int temp = Convert.ToInt32(result);
                    Value = temp == 1 ? "Centered" : temp == 2 ? "Co-sited" : result; break;
                default: Value = result; break;
            }
        }


        private string FindDataValue()
        {
            int num_items, item_size;
            string result = string.Empty;

            switch (DataType)
            {
                case ExifPropertyDataTypes.ByteArray:                                           // Byte array
                case ExifPropertyDataTypes.UByteArray:
                    result = BitConverter.ToString(DataBuffer);
                    break;

                case ExifPropertyDataTypes.String:                                              // String
                    result = Encoding.UTF8.GetString(DataBuffer, 0, DataLength - 1);
                    break;

                case ExifPropertyDataTypes.UShortArray:
                    item_size = 2;
                    num_items = DataLength / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        ushort value = BitConverter.ToUInt16(DataBuffer, i * item_size);
                        result += ", " + value.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    //Value = "[" + result + "]";
                    break;

                case ExifPropertyDataTypes.ULongArray:
                    item_size = 4;
                    num_items = DataLength / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        uint value = BitConverter.ToUInt32(DataBuffer, i * item_size);
                        result += ", " + value.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    //Value = "[" + result + "]";
                    break;

                case ExifPropertyDataTypes.ULongFractionArray:
                    item_size = 8;
                    num_items = DataLength / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        uint numerator = BitConverter.ToUInt32(DataBuffer, i * item_size);
                        uint denominator = BitConverter.ToUInt32(DataBuffer, i * item_size + item_size / 2);
                        //result += ", " + numerator.ToString() + "/" + denominator.ToString();
                        double val = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);
                        result += ", " + val.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    // Value = "[" + result + "]";
                    break;

                case ExifPropertyDataTypes.LongArray:
                    item_size = 4;
                    num_items = DataLength / item_size;

                    for (int i = 0; i < num_items; i++)
                    {
                        int value = BitConverter.ToInt32(DataBuffer, i * item_size);
                        result += ", " + value.ToString();
                        //result += ", " + Encoding.UTF8.GetString(DataBuffer);

                    }
                    if (result.Length > 0) result = result.Substring(2);
                    // Value = "[" + result + "]";
                    break;

                case ExifPropertyDataTypes.LongFractionArray:
                    item_size = 8;
                    num_items = DataLength / item_size;
                    for (int i = 0; i < num_items; i++)
                    {
                        int numerator = BitConverter.ToInt32(DataBuffer, i * item_size);
                        int denominator = BitConverter.ToInt32(DataBuffer, i * item_size + item_size / 2);
                        //result += ", " + numerator.ToString() + "/" + denominator.ToString();
                        double val = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);
                        result += ", " + val.ToString();
                    }
                    if (result.Length > 0) result = result.Substring(2);
                    // Value = "[" + result + "]";
                    break;
                default:
                    for (int i = 0; i < DataBuffer.Length; i++)
                    {
                        int value = DataBuffer[i];
                        result += value.ToString();
                        if (i != DataBuffer.Length - 1) result += ", ";
                        //result += ", " + Encoding.UTF8.GetString(DataBuffer);
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Find the image rotation
        /// </summary>
        /// <param name="result">The EXIF rotation value</param>
        /// <returns>Rotation description</returns>
        private string FindImageRotation(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 1: Value = "Horizontal (normal)"; break;
                case 2: Value = "Mirror Horizontal"; break;
                case 3: Value = "Rotate 180"; break;
                case 4: Value = "Mirror Vertical"; break;
                case 5: Value = "Mirror Horizontal & Rotate 270 CW"; break;
                case 6: Value = "Rotate 90 CW"; break;
                case 7: Value = "Mirror Horizontal and Rotate 90 CW"; break;
                case 8: Value = "Rotate 270 CW"; break;
                default: Value = "Unknown Orientation"; break;
            }
            return Value;
        }

        /// <summary>
        /// Find the metering value
        /// </summary>
        /// <param name="result">The EXIF metering value</param>
        /// <returns>Metering value as a human readable string</returns>
        private string FindMeteringMode(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: Value = "Unknown"; break;
                case 1: Value = "Average"; break;
                case 2: Value = "Center Weighted Average"; break;
                case 3: Value = "Spot"; break;
                case 4: Value = "Multi Spot"; break;
                case 5: Value = "Pattern / Multi Segment"; break;
                case 6: Value = "Partial"; break;
                case 255: Value = "Other"; break;
                default: Value = "Reserved"; break;
            }
            return Value;
        }

        /// <summary>
        /// Find flash value
        /// </summary>
        /// <param name="result">The EXIF flash value</param>
        /// <returns>Human readabe flash value</returns>
        private string FindFlashValue(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0x0: Value = "No Flash"; break;
                case 0x1: Value = "Flash Fired"; break;
                case 0x5: Value = "Fired, Return Not Detected"; break;
                case 0x7: Value = "Fired, Return Detected"; break;
                case 0x8: Value = "On, Did Not Fire"; break;
                case 0x9: Value = "On, Fired"; break;
                case 0xd: Value = "On, Return Not Detected"; break;
                case 0xf: Value = "On, Return Detected"; break;

                case 0x10: Value = "Off, Did Not Fire"; break;
                case 0x14: Value = "Off, Did Not Fire, Return Not Detected"; break;
                case 0x18: Value = "Auto, Did Not Fire"; break;
                case 0x19: Value = "Auto, Fired"; break;
                case 0x1d: Value = "Auto, Fired, Return Not Detected"; break;
                case 0x1f: Value = "Auto, Fired, Return Detected"; break;

                case 0x20: Value = "No Flash Function"; break;
                case 0x30: Value = "Off, No Flash Function"; break;
                case 0x41: Value = "Fired, Red-Eye Reduction"; break;
                case 0x45: Value = "Fired, Red-Eye Reduction, Return Not Detected"; break;
                case 0x47: Value = "Fired, Red-Eye Reduction, Return Detected"; break;
                case 0x49: Value = "On, Red-Eye Reduction"; break;
                case 0x4d: Value = "On, Red-eye reduction, Return not detected"; break;
                case 0x4f: Value = "On, Red-eye reduction, Return detected"; break;

                case 0x50: Value = "Off, Red-eye reduction"; break;
                case 0x58: Value = "Auto, Did not fire, Red-eye reduction"; break;
                case 0x59: Value = "Fired, Red-Eye Reduction"; break;
                case 0x5d: Value = "Auto, Fired, Red-eye reduction, Return not detected"; break;
                case 0x5f: Value = "Auto, Fired, Red-eye reduction, Return detected"; break;
                default: break;
            }
            return Value;
        }

        /// <summary>
        /// Image compression configuration
        /// </summary>
        /// <returns>RGB or YCbCr</returns>
        private string FindCompressionConfiguration()
        {
            string result = string.Empty;

            for (int i = 0; i < DataBuffer.Length; i++)
            {
                switch (DataBuffer[i])
                {
                    case 0: result += string.Empty; break;
                    case 1: result += "Y"; break;
                    case 2: result += "Cb"; break;
                    case 3: result += "Cr"; break;
                    case 4: result += "R"; break;
                    case 5: result += "G"; break;
                    case 6: result += "B"; break;
                    default: result += BitConverter.ToInt32(DataBuffer, i * DataBuffer.Length).ToString(); break;
                }
            }
            return result;
        }

        private string FindMakerNotes(string result)
        {
            return result;
            // need to implement maker notes
            //https://sno.phy.queensu.ca/~phil/exiftool/TagNames/EXIF.html#Flash
        }

        /// <summary>
        /// Image color space
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindColorSpace(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0x1: return "sRGB";
                case 0x2: return "Adobe RGB";
                case 0xfffd: return "Wide Gamut RGB";
                case 0xfffe: return "ICC Profile";
                case 0xffff: return "Uncalibrated";
                default: return result;
            }
        }

        /// <summary>
        /// Sensing method
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindSensingMethod(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 1: return "Not Defined";
                case 2: return "One-chip color area";
                case 3: return "Two-chip color area";
                case 4: return "Three-chip color area";
                case 5: return "Color sequential area";
                case 7: return "Trilinear";
                case 8: return "Color sequential linear";
                default: return result;
            }
        }

        /// <summary>
        /// Find Exposure Mode
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindExposureMode(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: return "Auto";
                case 1: return "Manual";
                case 2: return "Auto Bracket";
                default: return result;
            }
        }

        /// <summary>
        /// Find Scene Capture Type
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindSceneCaptureType(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: return "Standard";
                case 1: return "Landscape";
                case 2: return "Portrait";
                case 3: return "Night";
                case 4: return "Other";
                default: return result;
            }
        }

        /// <summary>
        /// Find GPS Speed Reference
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindGPSSpeedReference(string result)
        {
            switch (result)
            {
                case "K": return "Kilometers / Hour";
                case "M": return "Miles / Hour";
                case "N": return "Knots";
                default: return result;
            }
        }

        /// <summary>
        /// Find Exposure Program: https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifexposureprog
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string FindExposureProgram(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: return "Not Defined";
                case 1: return "Manual";
                case 2: return "Normal Program";
                case 3: return "Apature Priority";
                case 4: return "Shutter Priority";
                case 5: return "Creative Program (Biased toward depth of field)";
                case 6: return "Action Program (Biased toward fast shutter speed)";
                case 7: return "Portrait Mode (For close-up photos with background out of focus)";
                case 8: return "Landscape Mode (For landscape photos with the background in focus)";
                default: return result;
            }
        }

        /// <summary>
        /// Convert date to human readable date
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Human readable date</returns>
        private string FindDateTime(string result)
        {
            string[] vals = result.Split(' ');
            string date = vals[0].Replace(':', '/');
            string time = vals[1];
            DateTime dateTime = DateTime.Parse(date + " " + time);
            return dateTime.ToLongDateString() + " " + dateTime.ToLongTimeString();
        }


        private string FindSensitivityType(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: return "Unknown";
                case 1: return "Standard Output Sensitivity";
                case 2: return "Recommended Exposure Index";
                case 3: return "ISO Speed";
                case 4: return "Standard Output Sensitivity and Recommended Exposure Index";
                case 5: return "Standard Output Sensitivity and ISO Speed";
                case 6: return "Recomended Exposure Index and ISO Speed";
                case 7: return "Standard Output Sensitivity, Recomended Exposure Index and ISO Speed";
                default: return result;
            }
        }


        private string FindCustomRendered(string result)
        {
            switch (Convert.ToInt32(result))
            {
                case 0: return "Normal";
                case 1: return "Custom";
                case 3: return "HDR";
                case 6: return "Panorama";
                case 8: return "Portrait";
                default: return result;
            }
        }


        /// <summary>
        /// Creates a dictionary of most of the EXIF tags and their integer value
        /// </summary>
        public void CreatePropertyTags()
        {
            Dictionary<int, string> _PropertyTags = new Dictionary<int, string>() {
                #region ---- GPS ----
                { 0x0000, "GPS Version" },
                { 0x0001, "GPS Latitude Reference" },
                { 0x0002, "GPS Latitude" },
                { 0x0003, "GPS Longitude Reference" },
                { 0x0004, "GPS Longitude" },
                { 0x0005, "GPS Altitude Reference" },
                { 0x0006, "GPS Altitude" },
                { 0x0007, "GPS Time" },
                { 0x0008, "GPS Satelites" },
                { 0x0009, "GPS Status" },

                { 0x000A, "GPS Measure Mode" },
                { 0x000B, "GPS Degree Of Precision" },
                { 0x000C, "GPS Speed Reference" },
                { 0x000D, "GPS Speed" },
                { 0x000E, "GPS True North Reference" },
                { 0x000F, "GPS True North Degrees" },
                { 0x0010, "GPS Image Direction Reference" },
                { 0x0011, "GPS Image Direction" },
                { 0x0012, "GPS Map Datum" },
                { 0x0013, "GPS Destination Latitude Reference" },

                { 0x0014, "GPS Destination Latitude Direction" },
                { 0x0015, "GPS Destination Longitude Reference" },
                { 0x0016, "GPS Destination Longitude Direction" },
                { 0x0017, "GPS Destination Bearing Reference" },
                { 0x0018, "GPS Destination Bearing Direction" },
                { 0x0019, "GPS Distance To Destination Reference" },
                { 0x001A, "GPS Distance To Destination" },
#endregion

                { 0x00FE, "New Subfile Type" },
                { 0x00FF, "Subfile Type" },
                { 0x0100, "Image Width" },
                { 0x0101, "Image Height" },

                { 0x0102, "Bits Per Sample" },
                { 0x0103, "Compression" },
                { 0x0106, "Photometric Interp" },
                { 0x0107, "Thresh holding" },

                { 0x0108, "Cell Width" },
                { 0x0109, "Cell Height" },
                { 0x010A, "Fill Order" },
                { 0x010D, "Document Name" },

                { 0x010E, "Image Description" },
                { 0x010F, "Equipment Make" },
                { 0x0110, "Equipment Model" },
                { 0x0111, "Strip Offsets" },

                { 0x0112, "Orientation" },              //https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagorientation
                { 0x0115, "Samples Per Pixel" },
                { 0x0116, "Rows Per Strip" },
                { 0x0117, "Strip Bytes Count" },

                { 0x0118, "Minimum Sample Value" },
                { 0x0119, "Maximum Sample Value" },
                { 0x011A, "X Resolution" },
                { 0x011B, "Y Resolution" },

                { 0x011C, "Planar Configuration" },
                { 0x011D, "Page Name" },
                { 0x011E, "X Position" },
                { 0x011F, "Y Position" },

                { 0x0120, "Free Offset" },
                { 0x0121, "Free Byte Counts" },
                { 0x0122, "Gray Response Unit" },       // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytaggrayresponseunit
                { 0x0123, "Gray Response Curve" },
                { 0x0124, "T4 Encoding" },
                { 0x0125, "T6 Encoding" },

                { 0x0128, "Resolution Unit" },
                { 0x0129, "Page Number" },
                { 0x012D, "Transfer Function" },
                { 0x0131, "Software Used" },
                { 0x0132, "Date Time" },
                { 0x013B, "Artist" },
                { 0x013C, "Host Computer" },
                { 0x013D, "Predictor" },
                { 0x013E, "White Point" },
                { 0x013F, "Primary Chromaticities" },

                { 0x0140, "Color Map" },
                { 0x0141, "Halftone Hints" },
                { 0x0142, "Tile Width" },
                { 0x0143, "Tile Length" },
                { 0x0144, "Tile Offset" },
                { 0x0145, "Tile Byte Counts" },
                { 0x014C, "Ink Set" },
                { 0x014D, "Ink Names" },
                { 0x014E, "Nunber Of Inks" },

                { 0x0150, "Dot Range" },
                { 0x0151, "Target Printer" },
                { 0x0152, "Extra Samples" },
                { 0x0153, "Sample Format" },
                { 0x0154, "S Minimum Sample Value" },
                { 0x0155, "S Maximum Sample Value" },
                { 0x0156, "Transfer Range" },

                #region ---- JPEG ----
                { 0x0200, "JPEG Compression Process" },
                { 0x0201, "JPEG Interchange Format" },
                { 0x0202, "JPEG Interchange Length" },
                { 0x0203, "JPEG Restart Interval" },
                { 0x0205, "JPEG Lossless Predictors" },
                { 0x0206, "JPEG Point Transforms" },
                { 0x0207, "JPEG Q Tables" },
                // stopped here: https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagjpegdctables
                // started here: https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagindextransparent

#endregion 
                { 0x5110, "Pixel Unit" },
                { 0x5111, "Pixel Per Unit X" },
                { 0x5112, "Pixel Per Unit Y" },
                { 0x5113, "Palette Histogram" },
                { 0x8298, "Copyright" },

                #region ---- EXIF ----
                { 0x829A, "EXIF Exposure Time" },
                { 0x829D, "EXIF F Number" },

                { 0x8769, "EXIF IFD" },
                { 0x8773, "ICC Profile" },


                { 0x8822, "EXIF Exposure Prog" },       // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifexposureprog
                // stopped here: https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifspectralsense
                // started here: https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytaggpsifd

                { 0x8827, "EXIF ISO Speed" },
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifoecf
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifcompbpp
                { 0x9201, "EXIF Shutter Speed" },
                { 0x9202, "EXIF Apature" },
                { 0x9203, "EXIF Brightness" },
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifexposurebias
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifflash
                { 0x920A, "EXIF Focal Length" },
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifmakernote
                { 0x9286, "EXIF User Comment" },
                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifdtsubsec

                { 0xA001, "EXIF Color Space" },
                { 0x5039, "YCbCrPositioning" },
                { 0x9000, "EXIF Version" },
                { 0x9003, "EXIF Original Date Time" },
                { 0x9004, "EXIF Digitized Date Time" },
                { 0x9101, "EXIF Compression configuration" },
                { 0x9204, "EXIF Exposure Bias" },
                { 0x9207, "EXIF Metering Mode" },
                { 0x9209, "EXIF Flash" },
                { 0x927C, "EXIF Maker Note" },
#endregion

                { 0x9214, "Subject Area" },
                { 0x9291, "Sub Sec Time Original" },
                { 0x9292, "Sub Sec Time Digitized" },
                { 0xA000, "Flash Pix Version" },
                { 0xA002, "Exif Image Width" },
                { 0xA003, "Exif Image Height" },
                { 0xA217, "Sensing Method" },
                { 0xA301, "Scene Type" },
                { 0xA402, "Exposure Mode" },
                { 0xA403, "White Balance" },
                { 0xA405, "Focal Length In 35mm Format" },
                { 0xA406, "Scene Capture Type" },
                { 0xA432, "Lens Info" },
                { 0xA433, "Lens Make" },
                { 0xA434, "Lens Model" },

                { 0x1D, "64-bit RGBA Fixed Point" },
                { 0x1F, "64-bit CMYK" },
                { 0x0213, "Y Cb Cr Positioning" },            //1 = centered, 2 = Co-Sited
                { 0x501B, "Thumbnail Data" },
                { 0x5023, "Thumbnail Compression" },
                { 0x502D, "Thumbnail X Resolution" },
                { 0x502E, "Thumbnail Y Resolution" },
                { 0x5030, "Thumbnail Resolution Unit" },
                { 0x5091, "Chrominance Table" },
                { 0x5090, "Liminance Table" },
                { 0x9205, "Max Apature Value" },
                { 0x8830, "Sensitivity Type" },
                { 0x8832, "Recommended Exposure Index" },
                { 0xA20E, "Focal Plane X Resolution" },
                { 0xA20F, "Focal Plane Y Resolution" },
                { 0xA210, "Focal Plane Resolution Unit" },
                { 0xA401, "Custom Rendered" },
                { 0xA431, "Body Serial Number" },
                { 0xA435, "Lense Serial Number" },
                
                // animated gifs
                { 0x5100, "Frame Delay" },                  // number of frames in the image
                { 0x5101, "Loop Count" },                   // 0 = infinitely
                { 0x5102, "Global Color Palette" },
                { 0x5103, "Index Background" },
                { 0x5104, "Index Transparent" }
            };
            PropertyTags = _PropertyTags;

            PropertyResUnit = new Dictionary<int, string>()                // https://docs.microsoft.com/en-us/windows/desktop/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexiffocalresunit
            {
                { 1, "NA"},
                { 2, "Inch"},
                { 3, "CM"},
                { 4, "MM" },
                { 5, "UM" }
            };
        }

        #endregion
    }
}



/// <summary>
/// An Enum that identifies the EXIF data type
/// <Reference> https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-read-image-metadata </Reference>
/// </summary>
public enum ExifPropertyDataTypes : short
{
    PixelFormat4bppIndexed = 0,
    ByteArray = 1,
    String = 2,
    UShortArray = 3,
    ULongArray = 4,
    ULongFractionArray = 5,
    UByteArray = 6,
    LongArray = 7,
    Unused = 8,
    SLongArray = 9,
    LongFractionArray = 10,
}


///<summary>
/// The following Enumeration gives list (and descriptions) of the property items supported in EXIF format.
///</summary>	
public enum PropertyTagId : int
{
    ///<summary>Null-terminated character string that specifies the name of the person who created the image.</summary>
    Artist = 0x013B,
    ///<summary>Number of bits per color component. See also SamplesPerPixel.</summary>
    BitsPerSample = 0x0102,
    ///<summary></summary>
    CellHeight = 0x0109,
    ///<summary></summary>
    CellWidth = 0x0108,
    ///<summary></summary>
    ChrominanceTable = 0x5091,
    ///<summary></summary>
    ColorMap = 0x0140,
    ///<summary></summary>
    ColorTransferFunction = 0x501A,
    ///<summary></summary>
    Compression = 0x0103,
    ///<summary></summary>
    Copyright = 0x8298,
    ///<summary></summary>
    DateTime = 0x0132,
    ///<summary></summary>
    DocumentName = 0x010D,
    ///<summary></summary>
    DotRange = 0x0150,
    ///<summary></summary>
    Camera_Make = 0x010F,
    ///<summary></summary>
    Camera_Model = 0x0110,
    ///<summary></summary>
    ExifAperture = 0x9202,
    ///<summary></summary>
    ExifBrightness = 0x9203,
    ///<summary></summary>
    ExifCfaPattern = 0xA302,
    ///<summary></summary>
    ExifColorSpace = 0xA001,
    ///<summary></summary>
    ExifCompBPP = 0x9102,
    ///<summary></summary>
    ExifCompConfig = 0x9101,
    ///<summary></summary>
    ExifDTDigitized = 0x9004,
    ///<summary></summary>
    ExifDTDigSS = 0x9292,
    ///<summary></summary>
    ExifDTOrig = 0x9003,
    ///<summary></summary>
    ExifDTOrigSS = 0x9291,
    ///<summary></summary>
    ExifDTSubsec = 0x9290,
    ///<summary></summary>
    ExifExposureBias = 0x9204,
    ///<summary></summary>
    ExifExposureIndex = 0xA215,
    ///<summary></summary>
    ExifExposureProg = 0x8822,
    ///<summary></summary>
    ExifExposureTime = 0x829A,
    ///<summary></summary>
    ExifFileSource = 0xA300,
    ///<summary></summary>
    ExifFlash = 0x9209,
    ///<summary></summary>
    ExifFlashEnergy = 0xA20B,
    ///<summary></summary>
    ExifFNumber = 0x829D,
    ///<summary></summary>
    ExifFocalLength = 0x920A,
    ///<summary></summary>
    ExifFocalResUnit = 0xA210,
    ///<summary></summary>
    ExifFocalXRes = 0xA20E,
    ///<summary></summary>
    ExifFocalYRes = 0xA20F,
    ///<summary></summary>
    ExifFPXVer = 0xA000,
    ///<summary></summary>
    ExifIFD = 0x8769,
    ///<summary></summary>
    ExifInterop = 0xA005,
    ///<summary></summary>
    ExifISOSpeed = 0x8827,
    ///<summary></summary>
    ExifLightSource = 0x9208,
    ///<summary></summary>
    ExifMakerNote = 0x927C,
    ///<summary></summary>
    ExifMaxAperture = 0x9205,
    ///<summary></summary>
    ExifMeteringMode = 0x9207,
    ///<summary></summary>
    ExifOECF = 0x8828,
    ///<summary></summary>
    ExifPixXDim = 0xA002,
    ///<summary></summary>
    ExifPixYDim = 0xA003,
    ///<summary></summary>
    ExifRelatedWav = 0xA004,
    ///<summary></summary>
    ExifSceneType = 0xA301,
    ///<summary></summary>
    ExifSensingMethod = 0xA217,
    ///<summary></summary>
    ExifShutterSpeed = 0x9201,
    ///<summary></summary>
    ExifSpatialFR = 0xA20C,
    ///<summary></summary>
    ExifSpectralSense = 0x8824,
    ///<summary></summary>
    ExifSubjectDist = 0x9206,
    ///<summary></summary>
    ExifSubjectLoc = 0xA214,
    ///<summary></summary>
    ExifUserComment = 0x9286,
    ///<summary></summary>
    ExifVer = 0x9000,
    ///<summary></summary>
    ExtraSamples = 0x0152,
    ///<summary></summary>
    FillOrder = 0x010A,
    ///<summary></summary>
    FrameDelay = 0x5100,
    ///<summary></summary>
    FreeByteCounts = 0x0121,
    ///<summary></summary>
    FreeOffset = 0x0120,
    ///<summary></summary>
    Gamma = 0x0301,
    ///<summary></summary>
    GlobalPalette = 0x5102,
    ///<summary></summary>
    GpsAltitude = 0x0006,
    ///<summary></summary>
    GpsAltitudeRef = 0x0005,
    ///<summary></summary>
    GpsDestBear = 0x0018,
    ///<summary></summary>
    GpsDestBearRef = 0x0017,
    ///<summary></summary>
    GpsDestDist = 0x001A,
    ///<summary></summary>
    GpsDestDistRef = 0x0019,
    ///<summary></summary>
    GpsDestLat = 0x0014,
    ///<summary></summary>
    GpsDestLatRef = 0x0013,
    ///<summary></summary>
    GpsDestLong = 0x0016,
    ///<summary></summary>
    GpsDestLongRef = 0x0015,
    ///<summary></summary>
    GpsGpsDop = 0x000B,
    ///<summary></summary>
    GpsGpsMeasureMode = 0x000A,
    ///<summary></summary>
    GpsGpsSatellites = 0x0008,
    ///<summary></summary>
    GpsGpsStatus = 0x0009,
    ///<summary></summary>
    GpsGpsTime = 0x0007,
    ///<summary></summary>
    GpsIFD = 0x8825,
    ///<summary></summary>
    GpsImgDir = 0x0011,
    ///<summary></summary>
    GpsImgDirRef = 0x0010,
    ///<summary></summary>
    GpsLatitude = 0x0002,
    ///<summary></summary>
    GpsLatitudeRef = 0x0001,
    ///<summary></summary>
    GpsLongitude = 0x0004,
    ///<summary></summary>
    GpsLongitudeRef = 0x0003,
    ///<summary></summary>
    GpsMapDatum = 0x0012,
    ///<summary></summary>
    GpsSpeed = 0x000D,
    ///<summary></summary>
    GpsSpeedRef = 0x000C,
    ///<summary></summary>
    GpsTrack = 0x000F,
    ///<summary></summary>
    GpsTrackRef = 0x000E,
    ///<summary></summary>
    GpsVer = 0x0000,
    ///<summary></summary>
    GrayResponseCurve = 0x0123,
    ///<summary></summary>
    GrayResponseUnit = 0x0122,
    ///<summary></summary>
    GridSize = 0x5011,
    ///<summary></summary>
    HalftoneDegree = 0x500C,
    ///<summary></summary>
    HalftoneHints = 0x0141,
    ///<summary></summary>
    HalftoneLPI = 0x500A,
    ///<summary></summary>
    HalftoneLPIUnit = 0x500B,
    ///<summary></summary>
    HalftoneMisc = 0x500E,
    ///<summary></summary>
    HalftoneScreen = 0x500F,
    ///<summary></summary>
    HalftoneShape = 0x500D,
    ///<summary></summary>
    HostComputer = 0x013C,
    ///<summary></summary>
    ICCProfile = 0x8773,
    ///<summary></summary>
    ICCProfileDescriptor = 0x0302,
    ///<summary></summary>
    ImageDescription = 0x010E,
    ///<summary></summary>
    ImageHeight = 0x0101,
    ///<summary></summary>
    ImageTitle = 0x0320,
    ///<summary></summary>
    ImageWidth = 0x0100,
    ///<summary></summary>
    IndexBackground = 0x5103,
    ///<summary></summary>
    IndexTransparent = 0x5104,
    ///<summary></summary>
    InkNames = 0x014D,
    ///<summary></summary>
    InkSet = 0x014C,
    ///<summary></summary>
    JPEGACTables = 0x0209,
    ///<summary></summary>
    JPEGDCTables = 0x0208,
    ///<summary></summary>
    JPEGInterFormat = 0x0201,
    ///<summary></summary>
    JPEGInterLength = 0x0202,
    ///<summary></summary>
    JPEGLosslessPredictors = 0x0205,
    ///<summary></summary>
    JPEGPointTransforms = 0x0206,
    ///<summary></summary>
    JPEGProc = 0x0200,
    ///<summary></summary>
    JPEGQTables = 0x0207,
    ///<summary></summary>
    JPEGQuality = 0x5010,
    ///<summary></summary>
    JPEGRestartInterval = 0x0203,
    ///<summary></summary>
    LoopCount = 0x5101,
    ///<summary></summary>
    LuminanceTable = 0x5090,
    ///<summary></summary>
    MaxSampleValue = 0x0119,
    ///<summary></summary>
    MinSampleValue = 0x0118,
    ///<summary></summary>
    NewSubfileType = 0x00FE,
    ///<summary></summary>
    NumberOfInks = 0x014E,
    ///<summary></summary>
    Orientation = 0x0112,
    ///<summary></summary>
    PageName = 0x011D,
    ///<summary></summary>
    PageNumber = 0x0129,
    ///<summary></summary>
    PaletteHistogram = 0x5113,
    ///<summary></summary>
    PhotometricInterp = 0x0106,
    ///<summary></summary>
    PixelPerUnitX = 0x5111,
    ///<summary></summary>
    PixelPerUnitY = 0x5112,
    ///<summary></summary>
    PixelUnit = 0x5110,
    ///<summary></summary>
    PlanarConfig = 0x011C,
    ///<summary></summary>
    Predictor = 0x013D,
    ///<summary></summary>
    PrimaryChromaticities = 0x013F,
    ///<summary></summary>
    PrintFlags = 0x5005,
    ///<summary></summary>
    PrintFlagsBleedWidth = 0x5008,
    ///<summary></summary>
    PrintFlagsBleedWidthScale = 0x5009,
    ///<summary></summary>
    PrintFlagsCrop = 0x5007,
    ///<summary></summary>
    PrintFlagsVersion = 0x5006,
    ///<summary></summary>
    REFBlackWhite = 0x0214,
    ///<summary></summary>
    ResolutionUnit = 0x0128,
    ///<summary></summary>
    ResolutionXLengthUnit = 0x5003,
    ///<summary></summary>
    ResolutionXUnit = 0x5001,
    ///<summary></summary>
    ResolutionYLengthUnit = 0x5004,
    ///<summary></summary>
    ResolutionYUnit = 0x5002,
    ///<summary></summary>
    RowsPerStrip = 0x0116,
    ///<summary></summary>
    SampleFormat = 0x0153,
    ///<summary></summary>
    SamplesPerPixel = 0x0115,
    ///<summary></summary>
    SMaxSampleValue = 0x0155,
    ///<summary></summary>
    SMinSampleValue = 0x0154,
    ///<summary></summary>
    SoftwareUsed = 0x0131,
    ///<summary></summary>
    SRGBRenderingIntent = 0x0303,
    ///<summary></summary>
    StripBytesCount = 0x0117,
    ///<summary></summary>
    StripOffsets = 0x0111,
    ///<summary></summary>
    SubfileType = 0x00FF,
    ///<summary></summary>
    T4Option = 0x0124,
    ///<summary></summary>
    T6Option = 0x0125,
    ///<summary></summary>
    TargetPrinter = 0x0151,
    ///<summary></summary>
    ThreshHolding = 0x0107,
    ///<summary></summary>
    ThumbnailArtist = 0x5034,
    ///<summary></summary>
    ThumbnailBitsPerSample = 0x5022,
    ///<summary></summary>
    ThumbnailColorDepth = 0x5015,
    ///<summary></summary>
    ThumbnailCompressedSize = 0x5019,
    ///<summary></summary>
    ThumbnailCompression = 0x5023,
    ///<summary></summary>
    ThumbnailCopyRight = 0x503B,
    ///<summary></summary>
    ThumbnailData = 0x501B,
    ///<summary></summary>
    ThumbnailDateTime = 0x5033,
    ///<summary></summary>
    ThumbnailEquipMake = 0x5026,
    ///<summary></summary>
    ThumbnailEquipModel = 0x5027,
    ///<summary></summary>
    ThumbnailFormat = 0x5012,
    ///<summary></summary>
    ThumbnailHeight = 0x5014,
    ///<summary></summary>
    ThumbnailImageDescription = 0x5025,
    ///<summary></summary>
    ThumbnailImageHeight = 0x5021,
    ///<summary></summary>
    ThumbnailImageWidth = 0x5020,
    ///<summary></summary>
    ThumbnailOrientation = 0x5029,
    ///<summary></summary>
    ThumbnailPhotometricInterp = 0x5024,
    ///<summary></summary>
    ThumbnailPlanarConfig = 0x502F,
    ///<summary></summary>
    ThumbnailPlanes = 0x5016,
    ///<summary></summary>
    ThumbnailPrimaryChromaticities = 0x5036,
    ///<summary></summary>
    ThumbnailRawBytes = 0x5017,
    ///<summary></summary>
    ThumbnailRefBlackWhite = 0x503A,
    ///<summary></summary>
    ThumbnailResolutionUnit = 0x5030,
    ///<summary></summary>
    ThumbnailResolutionX = 0x502D,
    ///<summary></summary>
    ThumbnailResolutionY = 0x502E,
    ///<summary></summary>
    ThumbnailRowsPerStrip = 0x502B,
    ///<summary></summary>
    ThumbnailSamplesPerPixel = 0x502A,
    ///<summary></summary>
    ThumbnailSize = 0x5018,
    ///<summary></summary>
    ThumbnailSoftwareUsed = 0x5032,
    ///<summary></summary>
    ThumbnailStripBytesCount = 0x502C,
    ///<summary></summary>
    ThumbnailStripOffsets = 0x5028,
    ///<summary></summary>
    ThumbnailTransferFunction = 0x5031,
    ///<summary></summary>
    ThumbnailWhitePoint = 0x5035,
    ///<summary></summary>
    ThumbnailWidth = 0x5013,
    ///<summary></summary>
    ThumbnailYCbCrCoefficients = 0x5037,
    ///<summary></summary>
    ThumbnailYCbCrPositioning = 0x5039,
    ///<summary></summary>
    ThumbnailYCbCrSubsampling = 0x5038,
    ///<summary></summary>
    TileByteCounts = 0x0145,
    ///<summary></summary>
    TileLength = 0x0143,
    ///<summary></summary>
    TileOffset = 0x0144,
    ///<summary></summary>
    TileWidth = 0x0142,
    ///<summary></summary>
    TransferFunction = 0x012D,
    ///<summary></summary>
    TransferRange = 0x0156,
    ///<summary></summary>
    WhitePoint = 0x013E,
    ///<summary></summary>
    XPosition = 0x011E,
    ///<summary></summary>
    XResolution = 0x011A,
    ///<summary></summary>
    YCbCrCoefficients = 0x0211,
    ///<summary></summary>
    YCbCrPositioning = 0x0213,
    ///<summary></summary>
    YCbCrSubsampling = 0x0212,
    ///<summary></summary>
    YPosition = 0x011F,
    ///<summary></summary>
    YResolution = 0x011B
}


#region ---- REFERENCES ----

/* 
 * How to read Image Meta data: 
 *      https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-read-image-metadata
 *      https://blogs.msdn.microsoft.com/kamalds/2012/04/08/working-with-exif-metadata/
 * List of EXIF Tags: 
 *      https://sno.phy.queensu.ca/~phil/exiftool/TagNames/EXIF.html <- great resource
 *      https://www.media.mit.edu/pia/Research/deepview/exif.html
 *      https://www.exiv2.org/tags.html
 *      https://www.awaresystems.be/imaging/tiff/tifftags/privateifd/exif.html
 *      http://documentation.axiell.com/alm/en/ds_eiefimagedirectory.html
 * MSDN EXIF property descriptions: 
 *      https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions
 *      https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-image-property-tag-constants
 * 
 * https://stackoverflow.com/questions/16900291/c-sharp-image-propertyitems-metadate-how-do-you-know-which-number-is-which-pro
    0x0000 = GpsVer,
    0x0001 | GpsLatitudeRef
    0x0002 | GpsLatitude
    0x0003 | GpsLongitudeRef
    0x0004 | GpsLongitude
    0x0005 | GpsAltitudeRef
    0x0006 | GpsAltitude
    0x0007 | GpsGpsTime
    0x0008 | GpsGpsSatellites
    0x0009 | GpsGpsStatus
    0x000A | GpsGpsMeasureMode
    0x000B | GpsGpsDop
    0x000C | GpsSpeedRef
    0x000D | GpsSpeed
    0x000E | GpsTrackRef
    0x000F | GpsTrack
    0x0010 | GpsImgDirRef
    0x0011 | GpsImgDir
    0x0012 | GpsMapDatum
    0x0013 | GpsDestLatRef
    0x0014 | GpsDestLat
    0x0015 | GpsDestLongRef
    0x0016 | GpsDestLong
    0x0017 | GpsDestBearRef
    0x0018 | GpsDestBear
    0x0019 | GpsDestDistRef
    0x001A | GpsDestDist
    0x00FE | NewSubfileType
    0x00FF | SubfileType
    0x0100 | ImageWidth
    0x0101 | ImageHeight
    0x0102 | BitsPerSample
    0x0103 | Compression
    0x0106 | PhotometricInterp
    0x0107 | ThreshHolding
    0x0108 | CellWidth
    0x0109 | CellHeight
    0x010A | FillOrder
    0x010D | DocumentName
    0x010E | ImageDescription
    0x010F | EquipMake
    0x0110 | EquipModel
    0x0111 | StripOffsets
    0x0112 | Orientation
    0x0115 | SamplesPerPixel
    0x0116 | RowsPerStrip
    0x0117 | StripBytesCount
    0x0118 | MinSampleValue
    0x0119 | MaxSampleValue
    0x011A | XResolution
    0x011B | YResolution
    0x011C | PlanarConfig
    0x011D | PageName
    0x011E | XPosition
    0x011F | YPosition
    0x0120 | FreeOffset
    0x0121 | FreeByteCounts
    0x0122 | GrayResponseUnit
    0x0123 | GrayResponseCurve
    0x0124 | T4Option
    0x0125 | T6Option
    0x0128 | ResolutionUnit
    0x0129 | PageNumber
    0x012D | TransferFunction
    0x0131 | SoftwareUsed
    0x0132 | DateTime
    0x013B | Artist
    0x013C | HostComputer
    0x013D | Predictor
    0x013E | WhitePoint
    0x013F | PrimaryChromaticities
    0x0140 | ColorMap
    0x0141 | HalftoneHints
    0x0142 | TileWidth
    0x0143 | TileLength
    0x0144 | TileOffset
    0x0145 | TileByteCounts
    0x014C | InkSet
    0x014D | InkNames
    0x014E | NumberOfInks
    0x0150 | DotRange
    0x0151 | TargetPrinter
    0x0152 | ExtraSamples
    0x0153 | SampleFormat
    0x0154 | SMinSampleValue
    0x0155 | SMaxSampleValue
    0x0156 | TransferRange
    0x0200 | JPEGProc
    0x0201 | JPEGInterFormat
    0x0202 | JPEGInterLength
    0x0203 | JPEGRestartInterval
    0x0205 | JPEGLosslessPredictors
    0x0206 | JPEGPointTransforms
    0x0207 | JPEGQTables
    0x0208 | JPEGDCTables
    0x0209 | JPEGACTables
    0x0211 | YCbCrCoefficients
    0x0212 | YCbCrSubsampling
    0x0213 | YCbCrPositioning
    0x0214 | REFBlackWhite
    0x0301 | Gamma
    0x0302 | ICCProfileDescriptor
    0x0303 | SRGBRenderingIntent
    0x0320 | ImageTitle
    0x5001 | ResolutionXUnit
    0x5002 | ResolutionYUnit
    0x5003 | ResolutionXLengthUnit
    0x5004 | ResolutionYLengthUnit
    0x5005 | PrintFlags
    0x5006 | PrintFlagsVersion
    0x5007 | PrintFlagsCrop
    0x5008 | PrintFlagsBleedWidth
    0x5009 | PrintFlagsBleedWidthScale
    0x500A | HalftoneLPI
    0x500B | HalftoneLPIUnit
    0x500C | HalftoneDegree
    0x500D | HalftoneShape
    0x500E | HalftoneMisc
    0x500F | HalftoneScreen
    0x5010 | JPEGQuality
    0x5011 | GridSize
    0x5012 | ThumbnailFormat
    0x5013 | ThumbnailWidth
    0x5014 | ThumbnailHeight
    0x5015 | ThumbnailColorDepth
    0x5016 | ThumbnailPlanes
    0x5017 | ThumbnailRawBytes
    0x5018 | ThumbnailSize
    0x5019 | ThumbnailCompressedSize
    0x501A | ColorTransferFunction
    0x501B | ThumbnailData
    0x5020 | ThumbnailImageWidth
    0x5021 | ThumbnailImageHeight
    0x5022 | ThumbnailBitsPerSample
    0x5023 | ThumbnailCompression
    0x5024 | ThumbnailPhotometricInterp
    0x5025 | ThumbnailImageDescription
    0x5026 | ThumbnailEquipMake
    0x5027 | ThumbnailEquipModel
    0x5028 | ThumbnailStripOffsets
    0x5029 | ThumbnailOrientation
    0x502A | ThumbnailSamplesPerPixel
    0x502B | ThumbnailRowsPerStrip
    0x502C | ThumbnailStripBytesCount
    0x502D | ThumbnailResolutionX
    0x502E | ThumbnailResolutionY
    0x502F | ThumbnailPlanarConfig
    0x5030 | ThumbnailResolutionUnit
    0x5031 | ThumbnailTransferFunction
    0x5032 | ThumbnailSoftwareUsed
    0x5033 | ThumbnailDateTime
    0x5034 | ThumbnailArtist
    0x5035 | ThumbnailWhitePoint
    0x5036 | ThumbnailPrimaryChromaticities
    0x5037 | ThumbnailYCbCrCoefficients
    0x5038 | ThumbnailYCbCrSubsampling
    0x5039 | ThumbnailYCbCrPositioning
    0x503A | ThumbnailRefBlackWhite
    0x503B | ThumbnailCopyRight
    0x5090 | LuminanceTable
    0x5091 | ChrominanceTable
    0x5100 | FrameDelay
    0x5101 | LoopCount
    0x5102 | GlobalPalette
    0x5103 | IndexBackground
    0x5104 | IndexTransparent
    0x5110 | PixelUnit
    0x5111 | PixelPerUnitX
    0x5112 | PixelPerUnitY
    0x5113 | PaletteHistogram
    0x8298 | Copyright
    0x829A | ExifExposureTime
    0x829D | ExifFNumber
    0x8769 | ExifIFD
    0x8773 | ICCProfile
    0x8822 | ExifExposureProg
    0x8824 | ExifSpectralSense
    0x8825 | GpsIFD
    0x8827 | ExifISOSpeed
    0x8828 | ExifOECF
    0x9000 | ExifVer
    0x9003 | ExifDTOrig
    0x9004 | ExifDTDigitized
    0x9101 | ExifCompConfig
    0x9102 | ExifCompBPP
    0x9201 | ExifShutterSpeed
    0x9202 | ExifAperture
    0x9203 | ExifBrightness
    0x9204 | ExifExposureBias
    0x9205 | ExifMaxAperture
    0x9206 | ExifSubjectDist
    0x9207 | ExifMeteringMode
    0x9208 | ExifLightSource
    0x9209 | ExifFlash
    0x920A | ExifFocalLength
    0x927C | ExifMakerNote
    0x9286 | ExifUserComment
    0x9290 | ExifDTSubsec
    0x9291 | ExifDTOrigSS
    0x9292 | ExifDTDigSS
    0xA000 | ExifFPXVer
    0xA001 | ExifColorSpace
    0xA002 | ExifPixXDim
    0xA003 | ExifPixYDim
    0xA004 | ExifRelatedWav
    0xA005 | ExifInterop
    0xA20B | ExifFlashEnergy
    0xA20C | ExifSpatialFR
    0xA20E | ExifFocalXRes
    0xA20F | ExifFocalYRes
    0xA210 | ExifFocalResUnit
    0xA214 | ExifSubjectLoc
    0xA215 | ExifExposureIndex
    0xA217 | ExifSensingMethod
    0xA300 | ExifFileSource
    0xA301 | ExifSceneType
    0xA302 | ExifCfaPattern
*/

#endregion



