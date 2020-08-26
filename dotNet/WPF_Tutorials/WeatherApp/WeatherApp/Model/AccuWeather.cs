using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class Headline
    {
        public DateTime EffectiveDate { get; set; }
        public int EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public DateTime EndDate { get; set; }
        public int EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class TemperatureValue : INotifyPropertyChanged
    {
        private double val;
        public double Value
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged("Value");
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public int unitType;
        public int UnitType
        {
            get { return unitType; }
            set
            {
                unitType = value;
                OnPropertyChanged("UnitType");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class Temperature : INotifyPropertyChanged
    {
        private TemperatureValue minimum;
        public TemperatureValue Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                OnPropertyChanged("Minimum");
            }
        }

        private TemperatureValue maximum;
        public TemperatureValue Maximum
        {
            get { return maximum; }
            set {
                maximum = value;
                OnPropertyChanged("Maximum");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TimeOfDay : INotifyPropertyChanged
    {
        private int icon;
        public int Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        private string iconPhrase;
        public string IconPhrase
        {
            get { return iconPhrase; }

            set
            {
                iconPhrase = value;
                OnPropertyChanged("IconPhrase");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class DailyForecast: INotifyPropertyChanged
    {
        public List<string> Sources { get; set; }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private Temperature temperature;

        public Temperature Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        private TimeOfDay day;

        public TimeOfDay Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }

        private TimeOfDay night;

        public TimeOfDay Night
        {
            get { return night; }
            set
            {
                night = value;
                OnPropertyChanged("Night");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class AccuWeather
    {
        public List<DailyForecast> DailyForcasts { get; set; }

        public AccuWeather()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                DailyForcasts = new List<DailyForecast>();
                for (int i = 0; i < 3; i++)
                {
                    DailyForecast dailyForcast = new DailyForecast
                    {
                        Date = DateTime.Now.AddDays(-i),
                        Temperature = new Temperature
                        {
                            Maximum = new TemperatureValue { Value = 21 + i },
                            Minimum = new TemperatureValue { Value = 5 - i }
                        }
                    };
                    DailyForcasts.Add(dailyForcast);
                }
            }
        }
    }

}
