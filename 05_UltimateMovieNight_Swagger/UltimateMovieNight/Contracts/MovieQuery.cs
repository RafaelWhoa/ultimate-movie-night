using System;
using MongoDB.Bson.Serialization.Attributes;

namespace UltimateMovieNight.Contracts
{
	public class MovieQuery
	{
        public bool isAAPLTVChecked { get; set; }
		public bool isDISPChecked { get; set; }
		public bool isNFLXChecked { get; set; }
		public bool isAMZNChecked { get; set; }
		public bool isHBOChecked { get; set; }
        public bool isOutrosChecked { get; set; }
       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

