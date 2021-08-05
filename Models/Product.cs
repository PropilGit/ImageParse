using System;
using System.Collections.Generic;
using System.Text;

namespace ImageParse.Models
{
    class Product
    {
        public Product(int id, string name, string invNum, float count, string measureUnit)
        {
            Id = id;
            Name = name;
            InvNum = invNum;
            Count = count;
            MeasureUnit = measureUnit;
            
        }

        public Product(string text)
        {
            //1##АНТЕННА 3G TELEOFIS RC30##9834785    ##9##шт
            try
            {
                string[] attr = text.Split("##");
                if (attr.Length != 5) return;

                for (int i = 0; i < attr.Length; i++)
                {
                    attr[i] = attr[i].Trim();
                }

                Id = Int32.Parse(attr[0]);
                Name = attr[1];
                InvNum = attr[2];
                Count = float.Parse(attr[3]);
                MeasureUnit = attr[4];
            }
            catch (Exception)
            {

            }
            

        }

        public Product(int id, string name, string invNum, float count, string measureUnit, byte[] image)
        {
            Id = id;
            Name = name;
            InvNum = invNum;
            Count = count;
            MeasureUnit = measureUnit;
            Image = image;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string InvNum { get; private set; }
        public float Count { get; private set; }
        public string MeasureUnit { get; private set; }
        public byte[] Image { get; set; }

    }
}
