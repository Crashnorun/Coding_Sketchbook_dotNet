using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace AccessDB2JSON
{
    public class GlassSample
    {

        #region ---- PROPERTIES ----

        [JsonProperty(PropertyName ="label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }


        [JsonProperty(PropertyName = "glassBase1")]
        public string GlassBase { get; set; }

        [JsonProperty(PropertyName = "glassBase2")]
        public string GlassBase2 { get; set; }

        [JsonProperty(PropertyName = "glassBase3")]
        public string GlassBase3 { get; set; }


        [JsonProperty(PropertyName = "baseSpec1")]
        public string BaseSpec { get; set; }

        [JsonProperty(PropertyName = "baseSpec2")]
        public string BaseSpec2 { get; set; }

        [JsonProperty(PropertyName = "baseSpec3")]
        public string BaseSpec3 { get; set; }


        [JsonProperty(PropertyName = "glassThick1")]
        public string GlassThick1 { get; set; }

        [JsonProperty(PropertyName = "glassThick2")]
        public string GlassThick2 { get; set; }

        [JsonProperty(PropertyName = "glassThick3")]
        public string GlassThick3 { get; set; }


        [JsonProperty(PropertyName = "coating")]
        public string Coating { get; set; }

        [JsonProperty(PropertyName = "coating2")]
        public string coating2 { get; set; }


        [JsonProperty(PropertyName = "coatingSurface")]
        public string CoatingSurface { get; set; }

        [JsonProperty(PropertyName = "coatingSurface2")]
        public string CoatingSurface2 { get; set; }

        [JsonProperty(PropertyName = "surfaeModif")]
        public string SurfaceModif { get; set; }

        [JsonProperty(PropertyName = "lamination")]
        public string Lamination { get; set; }


        [JsonProperty(PropertyName = "transmittance")]
        public string Transmittance { get; set; }


        [JsonProperty(PropertyName = "reflectExt")]
        public string ReflectExt { get; set; }

        [JsonProperty(PropertyName = "reflectInt")]
        public string ReflectInt { get; set; }

        [JsonProperty(PropertyName = "Uvalue")]
        public double UValue { get; set; }


        [JsonProperty(PropertyName = "SHGC")]
        public double SHGC { get; set; }

        [JsonProperty(PropertyName = "LSG")]
        public double LSG { get; set; }

        [JsonProperty(PropertyName = "sampleDimensions")]
        public string SampleDimensions { get; set; }

        [JsonProperty(PropertyName = "checkedOut")]
        public bool CheckedOut { get; set; }

        [JsonProperty(PropertyName ="specDocs")]
        public List<object> SpecDocs { get; set; }
        #endregion

        public GlassSample() { }


    }
}
