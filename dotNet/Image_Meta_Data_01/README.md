# Image Exif Data

The goal of this project is to read image EXIF data

![Screen Capture 01](https://github.com/Crashnorun/Coding_Sketchbook/blob/master/dotNet/Image_Meta_Data_01/Imges/Screen_Capture_01.jpg)


## Supported file formats
* BMP
* JPG
* PNG
* GIF
* TIFF

## Supported EXIF tags:
| Tag Name | Tag ID | Type | Description |
|----|----|----|----|
Artist                  | 0x013B | String
BitsPerSample           | 0x0102 | Int16      | Number of bits per color component. See also SamplesPerPixel
CellHeight              | 0x0109 | Int16
CellWidth               | 0x0108 | Int16 
ChrominanceTable        | 0x5091
ColorMap                | 0x0140
ColorTransferFunction   | 0x501A
Compression             | 0x0103 | Int16    | [Exif compression values](https://exiftool.org/TagNames/EXIF.html#Compression)
Copyright               | 0x8298 | String   | May contain copyright info from the photographer or the editor
DateTime                | 0x0132 | String   | Modify Date
DocumentName            | 0x010D | String
DotRange                | 0x0150
Camera_Make             | 0x010F | string
Camera_Model            | 0x0110 | string
ExifAperture            | 0x9202 | rational64 | F stop value
ExifBrightness          | 0x9203 | rational64
ExifCfaPattern          | 0xA302 | undef
ExifColorSpace          | 0xA001 | Int16 | <ul> <li> 0x1 = sRGB </li> <li> 0x2 = Adobe RGB </li> <li> 0xfffd = Wide Gamut RGB </li> <li>  0xfffe = ICC Profile </li> <li> 0xffff = Uncalibrated </li> </ul>
ExifCompBPP             | 0x9102 | rational64
ExifCompConfig          | 0x9101 | undef | <ul> <li> 0 = - </li> <li> 1 = Y </li> <li> 2 = Cb </li> <li> 3 = Cr </li> <li>	4 = R </li> <li> 5 = G </li> <li> 6 = B </li> </ul>
ExifDTDigitized         | 0x9004 | string | date time digitized
ExifDTDigSS             | 0x9292 | string | fractional seconds for Create Date
ExifDTOrig              | 0x9003 | string | date time when orignal photo was taken
ExifDTOrigSS            | 0x9291 | string | fractional seconds for Original date time
ExifDTSubsec            | 0x9290 | string | fractional seconds for Modify Date
ExifExposureBias        | 0x9204 | string | exposure bias value 
ExifExposureIndex       | 0xA215 | rational64


**TO DO: Finish adding these to the table above**

    ExifExposureProg = 0x8822,
    ExifExposureTime = 0x829A,
    ExifFileSource = 0xA300,
    ExifFlash = 0x9209,
    ExifFlashEnergy = 0xA20B,
    ExifFNumber = 0x829D,
    ExifFocalLength = 0x920A,
    ExifFocalResUnit = 0xA210,
    ExifFocalXRes = 0xA20E,
    ExifFocalYRes = 0xA20F,
    ExifFPXVer = 0xA000,
    ExifIFD = 0x8769,
    ExifInterop = 0xA005,
    ExifISOSpeed = 0x8827,
    ExifLightSource = 0x9208,
    ExifMakerNote = 0x927C,
    ExifMaxAperture = 0x9205,
    ExifMeteringMode = 0x9207,
    ExifOECF = 0x8828,
    ExifPixXDim = 0xA002,
    ExifPixYDim = 0xA003,
    ExifRelatedWav = 0xA004,
    ExifSceneType = 0xA301,
    ExifSensingMethod = 0xA217,
    ExifShutterSpeed = 0x9201,
    ExifSpatialFR = 0xA20C,
    ExifSpectralSense = 0x8824,
    ExifSubjectDist = 0x9206,
    ExifSubjectLoc = 0xA214,
    ExifUserComment = 0x9286,
    ExifVer = 0x9000,
    ExtraSamples = 0x0152,
    FillOrder = 0x010A,
    FrameDelay = 0x5100,
    FreeByteCounts = 0x0121,
    FreeOffset = 0x0120,
    Gamma = 0x0301,
    GlobalPalette = 0x5102,
    GpsAltitude = 0x0006,
    GpsAltitudeRef = 0x0005,
    GpsDestBear = 0x0018,
    GpsDestBearRef = 0x0017,
    GpsDestDist = 0x001A,
    GpsDestDistRef = 0x0019,
    GpsDestLat = 0x0014,
    GpsDestLatRef = 0x0013,
    GpsDestLong = 0x0016,
    GpsDestLongRef = 0x0015,
    GpsGpsDop = 0x000B,
    GpsGpsMeasureMode = 0x000A,
    GpsGpsSatellites = 0x0008,
    GpsGpsStatus = 0x0009,
    GpsGpsTime = 0x0007,
    GpsIFD = 0x8825,
    GpsImgDir = 0x0011,
    GpsImgDirRef = 0x0010,
    GpsLatitude = 0x0002,
    GpsLatitudeRef = 0x0001,
    GpsLongitude = 0x0004,
    GpsLongitudeRef = 0x0003,
    GpsMapDatum = 0x0012,
    GpsSpeed = 0x000D,
    GpsSpeedRef = 0x000C,
    GpsTrack = 0x000F,
    GpsTrackRef = 0x000E,
    GpsVer = 0x0000,
    GrayResponseCurve = 0x0123,
    GrayResponseUnit = 0x0122,
    GridSize = 0x5011,
    HalftoneDegree = 0x500C,
    HalftoneHints = 0x0141,
    HalftoneLPI = 0x500A,
    HalftoneLPIUnit = 0x500B,
    HalftoneMisc = 0x500E,
    HalftoneScreen = 0x500F,
    HalftoneShape = 0x500D,
    HostComputer = 0x013C,
    ICCProfile = 0x8773,
    ICCProfileDescriptor = 0x0302,
    ImageDescription = 0x010E,
    ImageHeight = 0x0101,
    ImageTitle = 0x0320,
    ImageWidth = 0x0100,
    IndexBackground = 0x5103,
    IndexTransparent = 0x5104,
    InkNames = 0x014D,
    InkSet = 0x014C,
    JPEGACTables = 0x0209,
    JPEGDCTables = 0x0208,
    JPEGInterFormat = 0x0201,
    JPEGInterLength = 0x0202,
    JPEGLosslessPredictors = 0x0205,
    JPEGPointTransforms = 0x0206,
    JPEGProc = 0x0200,
    JPEGQTables = 0x0207,
    JPEGQuality = 0x5010,
    JPEGRestartInterval = 0x0203,
    LoopCount = 0x5101,
    LuminanceTable = 0x5090,
    MaxSampleValue = 0x0119,
    MinSampleValue = 0x0118,
    NewSubfileType = 0x00FE,
    NumberOfInks = 0x014E,
    Orientation = 0x0112,
    PageName = 0x011D,
    PageNumber = 0x0129,
    PaletteHistogram = 0x5113,
    PhotometricInterp = 0x0106,
    PixelPerUnitX = 0x5111,
    PixelPerUnitY = 0x5112,
    PixelUnit = 0x5110,
    PlanarConfig = 0x011C,
    Predictor = 0x013D,
    PrimaryChromaticities = 0x013F,
    PrintFlags = 0x5005,
    PrintFlagsBleedWidth = 0x5008,
    PrintFlagsBleedWidthScale = 0x5009,
    PrintFlagsCrop = 0x5007,
    PrintFlagsVersion = 0x5006,
    REFBlackWhite = 0x0214,
    ResolutionUnit = 0x0128,
    ResolutionXLengthUnit = 0x5003,
    ResolutionXUnit = 0x5001,
    ResolutionYLengthUnit = 0x5004,
    ResolutionYUnit = 0x5002,
    RowsPerStrip = 0x0116,
    SampleFormat = 0x0153,
    SamplesPerPixel = 0x0115,
    SMaxSampleValue = 0x0155,
    SMinSampleValue = 0x0154,
    SoftwareUsed = 0x0131,
    SRGBRenderingIntent = 0x0303,
    StripBytesCount = 0x0117,
    StripOffsets = 0x0111,
    SubfileType = 0x00FF,
    T4Option = 0x0124,
    T6Option = 0x0125,
    TargetPrinter = 0x0151,
    ThreshHolding = 0x0107,
    ThumbnailArtist = 0x5034,
    ThumbnailBitsPerSample = 0x5022,
    ThumbnailColorDepth = 0x5015,
    ThumbnailCompressedSize = 0x5019,
    ThumbnailCompression = 0x5023,
    ThumbnailCopyRight = 0x503B,
    ThumbnailData = 0x501B,
    ThumbnailDateTime = 0x5033,
    ThumbnailEquipMake = 0x5026,
    ThumbnailEquipModel = 0x5027,
    ThumbnailFormat = 0x5012,
    ThumbnailHeight = 0x5014,
    ThumbnailImageDescription = 0x5025,
    ThumbnailImageHeight = 0x5021,
    ThumbnailImageWidth = 0x5020,
    ThumbnailOrientation = 0x5029,
    ThumbnailPhotometricInterp = 0x5024,
    ThumbnailPlanarConfig = 0x502F,
    ThumbnailPlanes = 0x5016,
    ThumbnailPrimaryChromaticities = 0x5036,
    ThumbnailRawBytes = 0x5017,
    ThumbnailRefBlackWhite = 0x503A,
    ThumbnailResolutionUnit = 0x5030,
    ThumbnailResolutionX = 0x502D,
    ThumbnailResolutionY = 0x502E,
    ThumbnailRowsPerStrip = 0x502B,
    ThumbnailSamplesPerPixel = 0x502A,
    ThumbnailSize = 0x5018,
    ThumbnailSoftwareUsed = 0x5032,
    ThumbnailStripBytesCount = 0x502C,
    ThumbnailStripOffsets = 0x5028,
    ThumbnailTransferFunction = 0x5031,
    ThumbnailWhitePoint = 0x5035,
    ThumbnailWidth = 0x5013,
    ThumbnailYCbCrCoefficients = 0x5037,
    ThumbnailYCbCrPositioning = 0x5039,
    ThumbnailYCbCrSubsampling = 0x5038,
    TileByteCounts = 0x0145,
    TileLength = 0x0143,
    TileOffset = 0x0144,
    TileWidth = 0x0142,
    TransferFunction = 0x012D,
    TransferRange = 0x0156,
    WhitePoint = 0x013E,
    XPosition = 0x011E,
    XResolution = 0x011A,
    YCbCrCoefficients = 0x0211,
    YCbCrPositioning = 0x0213,
    YCbCrSubsampling = 0x0212,
    YPosition = 0x011F,
    YResolution = 0x011B


## Resources:


Found this Code Project article later on: [Access to EXIF tags in JPEG files](https://www.codeproject.com/Articles/5251929/Access-to-EXIF-tags-in-JPEG-files)
