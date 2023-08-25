using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCheckBook.DataStore
{
    internal class Address
    {
        // internal class to have shared data and methods for dealing with addresses



        internal static readonly string[] StateListAbreviations =
        {
            //Alabama
            "AL"
            //Alaska
            ,"AK"
            //American Samoa
            ,"AS"
            //Arizona
            ,"AZ"
            //Arkansas
            ,"AR"
            //California
            ,"CA"
            //Colorado
            ,"CO"
            //Connecticut
            ,"CT"
            //Delaware
            ,"DE"
            //District of Columbia
            ,"DC"
            //Federated States of Micronesia
            ,"FM"
            //Florida
            ,"FL"
            //Georgia
            ,"GA"
            //Guam
            ,"GU"
            //Hawaii
            ,"HI"
            //Idaho
            ,"ID"
            //Illinois
            ,"IL"
            //Indiana
            ,"IN"
            //Iowa
            ,"IA"
            //Kansas
            ,"KS"
            //Kentucky
            ,"KY"
            //Louisiana
            ,"LA"
            //Maine
            ,"ME"
            //Marshall Islands
            ,"MH"
            //Maryland
            ,"MD"
            //Massachusetts
            ,"MA"
            //Michigan
            ,"MI"
            //Minnesota
            ,"MN"
            //Mississippi
            ,"MS"
            //Missouri
            ,"MO"
            //Montana
            ,"MT"
            //Nebraska
            ,"NE"
            //Nevada
            ,"NV"
            //New Hampshire
            ,"NH"
            //New Jersey
            ,"NJ"
            //New Mexico
            ,"NM"
            //New York
            ,"NY"
            //North Carolina
            ,"NC"
            //North Dakota
            ,"ND"
            //Northern Mariana Islands
            ,"MP"
            //Ohio
            ,"OH"
            //Oklahoma
            ,"OK"
            //Oregon
            ,"OR"
            //Palau
            ,"PW"
            //Pennsylvania
            ,"PA"
            //Puerto Rico
            ,"PR"
            //Rhode Island
            ,"RI"
            //South Carolina
            ,"SC"
            //South Dakota
            ,"SD"
            //Tennessee
            ,"TN"
            //Texas
            ,"TX"
            //Utah
            ,"UT"
            //Vermont
            ,"VT"
            //Virgin Islands
            ,"VI"
            //Virginia
            ,"VA"
            //Washington
            ,"WA"
            //West Virginia
            ,"WV"
            //Wisconsin
            ,"WI"
            //Wyoming
            ,"WY"

            //Military “State” Abbreviation
            //Armed Forces Europe, the Middle East, and Canada
            ,"AE"
            //Armed Forces Pacific
            ,"AP"
            //Armed Forces Americas(except Canada)
            ,"AA"

            //CANADA

            //Alberta
            ,"AB"
            //British Columbia
            ,"BC"
            //Manitoba
            ,"MB"
            //New Brunswick
            ,"NB"
            //Newfoundland and Labrador
            ,"NL"
            // Northwest Territories
            ,"NT"
            //Nova Scotia
            ,"NS"
            // Nunavut
            ,"NU"
            //Ontario
            ,"ON"
            //Prince Edward Island
            ,"PE"
            //Quebec
            ,"QC"
            //Saskatchewan
            ,"SK"
            // Yukon
            ,"YT"


            //Mexico
            //Aguascalientes
            ,"AG"
            //Baja California
            ,"BC"
            //Baja California Sur
            ,"BS"
            // Campeche
            ,"CM"
            // Chiapas
            ,"CS"
            // Chihuahua
            ,"CH"
            // Coahuila
            ,"CO"
            // Colima
            ,"CL"
            // Distrito Federal
            ,"DF"
            // Durango
            ,"DG"
            // Guanajuanto
            ,"GT"
            // Guerrero
            ,"GR"
            // Hidalgo
            ,"HG"
            // Jalisco
            ,"JA"
            // Mexico
            ,"MX"
            // Mochoacan
            ,"MI"
            // Morelos
            ,"MO"
            // Nayarit
            ,"NA"
            // Nuevo Leon
            ,"NL"
            // Oaxaca
            ,"OA"
            // Puebla
            ,"PU"
            // Queretaro
            ,"QT"
            // Quintana Roo
            ,"QR"
            // San Luis Potosi
            ,"SL"
            // Sinola
            ,"SI"
            // Sonora
            ,"SO"
            // Tabasco
            ,"TB"
            // Tamaulipas
            ,"TM"
            // Tlaxcala
            ,"TL"
            // Veracruz
            ,"VE"
            // Yucatan
            ,"YU"
            // Zacatecas
            ,"ZA"
        };


        internal static readonly (string StateName, string StateAbreviation)[] StateList =
        {
            ("ALABAMA", "AL")
            ,("ALASKA","AK")
            ,("AMERICAN SAMOA","AS")
            ,("ARIZONA","AZ")
            ,("ARKANSAS","AR")
            ,("CALIFORNIA","CA")
            ,("COLORADO","CO")
            ,("CONNECTICUT","CT")
            ,("DELAWARE","DE")
            ,("DISTRICT OF COLUMBIA","DC")
            ,("FEDERATED STATES OF MICRONESIA","FM")
            ,("FLORIDA","FL")
            ,("GEORGIA","GA")
            ,("GUAM","GU")
            ,("HAWAII","HI")
            ,("IDAHO","ID")
            ,("ILLINOIS","IL")
            ,("INDIANA","IN")
            ,("IOWA","IA")
            ,("KANSAS","KS")
            ,("KENTUCKY","KY")
            ,("LOUISIANA","LA")
            ,("MAINE","ME")
            ,("MARSHALL ISLANDS","MH")
            ,("MARYLAND","MD")
            ,("MASSACHUSETTS","MA")
            ,("MICHIGAN","MI")
            ,("MINNESOTA","MN")
            ,("MISSISSIPPI","MS")
            ,("MISSOURI","MO")
            ,("MONTANA","MT")
            ,("NEBRASKA","NE")
            ,("NEVADA","NV")
            ,("NEW HAMPSHIRE","NH")
            ,("NEW JERSEY","NJ")
            ,("NEW MEXICO","NM")
            ,("NEW YORK","NY")
            ,("NORTH CAROLINA","NC")
            ,("NORTH DAKOTA","ND")
            ,("NORTHERN MARIANA ISLANDS","MP")
            ,("OHIO","OH")
            ,("OKLAHOMA","OK")
            ,("OREGON","OR")
            ,("PALAU","PW")
            ,("PENNSYLVANIA","PA")
            ,("PUERTO RICO","PR")
            ,("RHODE ISLAND","RI")
            ,("SOUTH CAROLINA","SC")
            ,("SOUTH DAKOTA","SD")
            ,("TENNESSEE","TN")
            ,("TEXAS","TX")
            ,("UTAH","UT")
            ,("VERMONT","VT")
            ,("VIRGIN ISLANDS","VI")
            ,("VIRGINIA","VA")
            ,("WASHINGTON","WA")
            ,("WEST VIRGINIA","WV")
            ,("WISCONSIN","WI")
            ,("WYOMING","WY")

            //Military “State” Abbreviation
            ,("ARMED FORCES EUROPE, THE MIDDLE EAST, AND CANADA","AE")
            ,("ARMED FORCES PACIFIC","AP")
            ,("ARMED FORCES AMERICAS","AA")

            //CANADA
            ,("ALBERTA","AB")
            ,("BRITISH COLUMBIA","BC")
            ,("MANITOBA","MB")
            ,("NEW BRUNSWICK","NB")
            ,("NEWFOUNDLAND AND LABRADOR","NL")
            ,("NORTHWEST TERRITORIES","NT")
            ,("NOVA SCOTIA","NS")
            ,("NUNAVUT","NU")
            ,("ONTARIO","ON")
            ,("PRINCE EDWARD ISLAND","PE")
            ,("QUEBEC","QC")
            ,("SASKATCHEWAN","SK")
            ,("YUKON","YT")
        

        //Mexico


            ,("AGUASCALIENTES","AG")
            ,("BAJA CALIFORNIA","BC")
            ,("BAJA CALIFORNIA SUR","BS")
            ,("CAMPECHE","CM")
            ,("CHIAPAS","CS")
            ,("CHIHUAHUA","CH")
            ,("COAHUILA","CO")
            ,("COLIMA","CL")
            ,("DISTRITO FEDERAL","DF")
            ,("DURANGO","DG")
            ,("GUANAJUANTO","GT")
            ,("GUERRERO","GR")
            ,("HIDALGO","HG")
            ,("JALISCO","JA")
            ,("MEXICO","MX")
            ,("MOCHOACAN","MI")
            ,("MORELOS","MO")
            ,("NAYARIT","NA")
            ,("NUEVO LEON","NL")
            ,("OAXACA","OA")
            ,("PUEBLA","PU")
            ,("QUERETARO","QT")
            ,("QUINTANA ROO","QR")
            ,("SAN LUIS POTOSI","SL")
            ,("SINOLA","SI")
            ,("SONORA","SO")
            ,("TABASCO","TB")
            ,("TAMAULIPAS","TM")
            ,("TLAXCALA","TL")
            ,("VERACRUZ","VE")
            ,("YUCATAN","YU")
            ,("ZACATECAS","ZA")
        };

        // Allowed Countries
        internal static readonly string[] AllowedCountry =
        {
            "USA",
            "CANADA",
            "MEXICO"
        };



        public Address() { }




        internal static List<string> GetStateAbreviations()
        { return StateListAbreviations.ToList();  }

        internal static bool IsValidCountry (string testCountry)
        {
            if (string.IsNullOrEmpty(testCountry)) return true;
            testCountry = testCountry.Trim().ToUpper();
            foreach(string country in AllowedCountry)
            {
                if (country.Equals(testCountry)) return true;
            }
            return false;
        }

        internal static string[] ParseAddressCityStateZipCountry(string testLine1, string testLine2, string testLine3)
        {
            string[] results = new string[6];
            results[0] = "";  // address1
            results[1] = "";  // address2
            results[2] = "";  // city
            results[3] = "";  // state
            results[4] = "";  // zip code
            results[5] = "";  // country

            string country;
            string withoutCountry;

            // see if we have a country in the lines and split it off
            if (ContainsCountry(testLine3, out country, out withoutCountry))
            {
                results[5] = country;
                testLine3 = withoutCountry;
            }
            if (testLine3.Length > 0)
            {
                    results[0] = testLine1;
                    results[1] = testLine2;
                    if (SplitCityStateZip(testLine3, out results[2], out results[3], out results[4])) return results;
            }

            // see what we have in the second line
            if (ContainsCountry(testLine2, out country, out withoutCountry))
            {
                results[5] = country;
                testLine2 = withoutCountry;
            }
            if (testLine2.Length > 0)
            {
                results[0] = testLine1;
                if (SplitCityStateZip(testLine2, out results[2], out results[3], out results[4])) return results;
            }


            return results;
        }
        private static bool ContainsCountry(string testString, out string country, out string withoutCountry)
        {
            country = "";
            withoutCountry = "";

            foreach (string possibleCountry in DataStore.Address.AllowedCountry)
                if (testString.ToUpper().Contains(possibleCountry))
                {
                    // if a single word. 
                    if (testString.Length == possibleCountry.Length)
                    {
                        country = possibleCountry;
                        withoutCountry = "";
                        return true;
                    }
                    //If longer than this country name, can't have letters on either side

                    // New Mexico is a state, not a country
                    if (possibleCountry == "MEXICO" && testString.ToUpper().Contains("NEW MEXICO")) continue;

                    int pos = testString.ToUpper().IndexOf(possibleCountry);
                    if (pos == 0)
                    {
                        country = possibleCountry;
                        withoutCountry = "";

                        if (testString[possibleCountry.Length] == ' ') return true;
                        if (testString[possibleCountry.Length] == ',') return true;
                    }

                    // if there is something at the start of this country name, then isn't a country
                    if (Char.IsLetter(testString[pos - 1])) continue;
                    if ((testString[pos - 1] != '\t') && (testString[pos - 1] != ' ') && (testString[pos - 1] != ',')) continue;

                    // nothing after this name, is a country
                    if (pos + possibleCountry.Length + 1 > testString.Length)
                    {
                        country = possibleCountry;
                        withoutCountry = testString[..pos];
                        return true;
                    }
                }
            return false;
        }

        private static bool SplitCityStateZip(string CityStateZip, out string city, out string state, out string zipcode)
        {
            city = "";
            state = "";
            zipcode = "";

            // split up what we think is the city state zip line


            // the city is easy, pull off the first before the comma

            int pos = CityStateZip.IndexOf(',');
            if (pos >= 0)
            {
                int start = 0;
                if (CityStateZip[start] == '"') start++;
                city = CityStateZip[start..pos];

                // now the hard part is getting the state
                // there might be a space, wanting two character state but there might be a full state name which might be several words

                pos++;
                if (CityStateZip.Length <= pos) return false;
                while (CityStateZip[pos] == ' ')
                {
                    pos++;
                    if (CityStateZip.Length <= pos) return false;
                }
                string testState = CityStateZip[pos..].ToUpper();

                foreach ((string StateName, string StateAbreviation) in StateList)
                {
                    if (testState.Contains(StateName))
                    {
                        state = StateAbreviation;
                        pos += StateName.Length;
                        break;
                    }
                    if (testState.Length > 3)
                        if ((testState[0..2] == StateAbreviation) && (testState[2] == ' '))
                        {
                            state = StateAbreviation;
                            pos += state.Length;
                            break;
                        }
                }


                // now pull off any zip code

                pos++;
                if (CityStateZip.Length <= pos) return false;
                if (CityStateZip[pos] == ' ') pos++;
                if (CityStateZip.Length <= pos) return false;
                zipcode = CityStateZip[pos..];
                if (zipcode[^1] == '"') zipcode = zipcode[..(zipcode.Length - 1)];
                return true;

            }
            return false;

        }
    }
}
