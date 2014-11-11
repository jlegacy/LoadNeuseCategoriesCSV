using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace LoadNeuseCSV
{
    internal class SectionCsv
    {
        [CsvColumn(Name = "id", FieldIndex = 1)]
        public int id { get; set; }

        [CsvColumn(Name = "parent", FieldIndex = 2)]
        public int parent { get; set; }

        [CsvColumn(Name = "level", FieldIndex = 3)]
        public int level { get; set; }

        [CsvColumn(Name = "type", FieldIndex = 4)]
        public string type { get; set; }

        [CsvColumn(Name = "code", FieldIndex = 5)]
        public string code { get; set; }

        [CsvColumn(Name = "description", FieldIndex = 6)]
        public string description { get; set; }

        [CsvColumn(Name = "sort", FieldIndex = 7)]
        public string sort { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            var cc = new CsvContext();

            IEnumerable<SectionCsv> sections =
                cc.Read<SectionCsv>("C:\\Users\\jlegacy\\Desktop\\BRScategory.csv", inputFileDescription);

// Data is now available via variable products.

            IEnumerable<SectionCsv> sectionsByName =
                from p in sections
                where p.level.Equals(3)
                select p;
// or ...
            foreach (SectionCsv item in sectionsByName)
            {
                //Data maping object to our database
                var text = new DataClasses1DataContext();
                var mySection = new section();

                buildData(mySection, item);
                text.sections.InsertOnSubmit(mySection);

                try
                {
                    text.SubmitChanges();
                }
                catch (Exception ex)
                {
                    
                }

                // executes the appropriate commands to implement the changes to the database
            }
        }

        private static void buildData(section mySection, SectionCsv item)
        {
            mySection.sectionID = Asc(item.code);
            mySection.sectionName = item.description;
            mySection.sectionName2 = "";
            mySection.sectionName3 = "";
            mySection.sectionWorkingName = item.description;
            mySection.sectionImage = "";
            mySection.topSection = 99;
            mySection.sectionOrder = item.id;
            mySection.rootSection = 1;
            mySection.sectionDisabled = 0;
            mySection.sectionurl = "";
            mySection.sectionurl2 = "";
            mySection.sectionurl3 = "";
            mySection.sectionHeader = item.description;
            mySection.sectionHeader2 = "";
            mySection.sectionHeader3 = "";
            mySection.sectionDescription = "";
            mySection.sectionDescription2 = "";
            mySection.sectionDescription3 = "";
            mySection.sTitle = "";
            mySection.sMetaDesc = "";
        }

     
           static int Asc(String ch)
        {
            //Return the character value of the given character
            ch = ch.ToUpper();
            var bytes = Encoding.ASCII.GetBytes(ch);
            var numString = "";
            int j;
            foreach (byte b in bytes)
            {
                if (b >= 48 && b <= 57)
                {
                    numString = numString + (Convert.ToChar(b)).ToString();
                }
                else
                {
                    j = b - 64;
                    numString = numString + (Convert.ToString(j));
                }
               

            }

            return Convert.ToInt32(numString);

        }
        }
    }
