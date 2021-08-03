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
