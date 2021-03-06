using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile_Two : ContentPage
    {
        public User user;
        public Profile_Two(object user)
        {
            InitializeComponent();

            var dayList = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31
            };
            
            var monthList = new List<string>()
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
            };

            var yearList = new List<string>()
            {
                "1903", "1904", "1905", "1906", "1907", "1908", "1909", "1910", "1911", "1912", "1913", "1914", "1915", "1916", "1917", "1918", "1919", "1920", "1921", "1922", "1923", "1924", "1925", "1926", "1927", "1928", "1929", "1930", "1931", "1932", "1933", "1934", "1935", "1936", "1937", "1938", "1939", "1940", "1941", "1942", "1943", "1944", "1945", "1946", "1947", "1948", "1949", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030", "2031", "2032", "2033", "2034", "2035", "2036", "2037", "2038", "2039", "2040", "2041", "2042", "2043", "2044", "2045", "2046", "2047", "2048"
            };

            var bodyList = new List<string>()
            {
                "Average", "Large", "Muscular", "Slim", "Stocky", "Toned", "Thin"
            };

            var skinList = new List<string>()
            {
                "Black", "Brown", "Dark Brown", "Light Brown", "White"
            };
            

            var eyeList = new List<string> (){
                "Blue", "Brown", "Green", "Hazel", "Grey", "Light Blue", "Light Brown", "Light Green", "Light Grey", "Light Purple", "Light Red", "Light Yellow", "Purple", "Red", "Yellow"
            };
        
            

            dayPicker.ItemsSource = dayList;
            monthPicker.ItemsSource = monthList;
            yearPicker.ItemsSource = yearList;
            bodyPicker.ItemsSource = bodyList;
            skinPicker.ItemsSource = skinList;
            eyePicker.ItemsSource = eyeList;

            this.user = (User)user.GetType().GetProperty("user").GetValue(user, null);
            this.BindingContext.GetType().GetProperty("user").SetValue(this.BindingContext, this.user);

        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            displayHeight.Text = String.Format("{0} cm", value);
        }

        
        private void Submit(object sender, EventArgs e)
        {
            

            if (dayPicker.SelectedItem == null || monthPicker.SelectedItem == null || yearPicker.SelectedItem == null || bodyPicker.SelectedItem == null || skinPicker.SelectedItem == null || eyePicker.SelectedItem == null)
            {
                DisplayAlert("Oops", "Please fill in all the fields", "OK");
            }else
            {
                Navigation.PushAsync(new Profile_Three(BindingContext));
            }
        }
    }



}