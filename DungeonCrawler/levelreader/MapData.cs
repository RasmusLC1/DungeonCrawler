using System.Collections.Generic;


namespace ZombieGame.Levels { 
    public class MapConverter {
        private List <string> level  = new List<string>();
        private LevelData leveldata = new LevelData();
        private List<string> MetaList = new List<string>();
        private List<string> legendList = new List<string>();
        private Dictionary<string, string> effects = new Dictionary<string, string>();
        private Dictionary<string, string> PowerUp = new Dictionary<string, string>();
        private List<string> map = new List<string>();
        private List<char> signs = new List<char>();
        private List<string> image = new List<string>();
        private TextConverter textconverter = new TextConverter();
        public List<string> getlevel {
            get {return level;}
        }
        
        public void MapDataSetup(string value){
            level = textconverter.LevelDictionary(value);
            legendList = leveldata.Legend(level);
            MetaList = leveldata.Meta(level);
        }
        public List<string> Legend2Image(){
            foreach (string element in legendList) {
                image.Add(element.Substring(3));
            }
            return image;
        }
        public List<char> Legend2Sign(){
            foreach (string element in legendList) {
                signs.Add(element[0]);
            }
            return signs;
        }
        public List<string> GetMap(){
            map = leveldata.Map(level);
            return map;
        }
    }
}