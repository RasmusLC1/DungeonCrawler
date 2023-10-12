using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ZombieGame.Levels {
    public class TextConverter {
        private List<string> Lines = new List<string>();
        private Dictionary<string, string> test = new Dictionary<string, string>();
        public List<string> LevelDictionary (string value) {
            Lines.Clear();
            Lines = File.ReadLines(Path.Combine("Levels", ""+value+".txt")).ToList();
            return Lines;
        }
    }
}