using System.Collections.Generic;


namespace ZombieGame.Levels { 
    public class LevelData {
        /// <summary>
        /// Extracts map data from text file.
        /// </summary>
        /// <returns>The map data</returns>
        public List<string> Map (List<string> Lines){
            List<string> map = new List<string>();
            int Startindex = Lines.IndexOf("Map:");
            int Endindex = Lines.IndexOf("Map/");
            for (int i = Startindex+1; i < Endindex; i++){
                map.Add(Lines[i]);
            }
            return map;
        }
        /// <summary>
        /// Extracts meta data from text file.
        /// </summary>
        /// <returns>The meta data</returns>
        public List<string> Meta (List<string> Lines){
            List<string> meta = new List<string>();
            int Startindex = Lines.IndexOf("Meta:");
            int Endindex = Lines.IndexOf("Meta/");
            for (int i = Startindex+1; i < Endindex; i++){
                meta.Add(Lines[i]);
            }
            return meta;
        }
        /// <summary>
        /// Extracts legend data from text tile.
        /// </summary>
        /// <returns>The legend data</returns>
        public List<string> Legend (List<string> Lines){
            List<string> legend = new List<string>();
            int Startindex = Lines.IndexOf("Legend:");
            int Endindex = Lines.IndexOf("Legend/");
            for (int i = Startindex+1; i < Endindex; i++){
                legend.Add(Lines[i]);
            }
            return legend;
        }
    }
}