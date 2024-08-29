using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChipSecuritySystem {
    class Program {
        // constants for initial and final chip colors so that they can be easily changed if needed
        private const Color INITIAL_COLOR = Color.Blue;
        private const Color FINAL_COLOR = Color.Green;

        // constant strings for error/success messages
        private const string INPUT_ERROR_MSG = "Invalid input, please try again!";
        private const string SUCCESS_MSG = "Unlocked!";

        static void Main(string[] args) {

            // read inputs passed in as program args and convert to list of ColorChips; print error message if invalid input
            List<ColorChip> inputChips = getChipsFromInput(args);
            if (inputChips == null || inputChips.Count == 0) {
                Console.WriteLine(INPUT_ERROR_MSG);
                return;
            }

            // create a dictionary, mapping startColor -> endColors; allows for faster, O(1), retrieval in the future
            IDictionary<Color, List<Color>> chipsDict = initializeChipsDictionary(inputChips);
            
            // start recursive tree search with the INITIAL_COLOR & print solution
            List<ColorChip> solution = searchTree(chipsDict, INITIAL_COLOR);
            printSolution(solution);
        }

        // method to parse the first arg into List<ColorChip>; return null for any input errors
        private static List<ColorChip> getChipsFromInput(string[] args) {
            // must be 1 arg input
            if (args.Length != 1) return null;

            // trim string and validate format 
            string userInput = args[0].Trim();
            if (!userInput.StartsWith("[") || !userInput.EndsWith("]")) return null;

            // remove []s and split string into bi-color pairs, separated by comma
            userInput = userInput.Substring(1, userInput.Length - 2);
            string[] chipStrings = Regex.Split(userInput, @"\]\s*\[");
            if (chipStrings == null || chipStrings.Length == 0) return null;

            // split each pair into sigular colors, validate against enum, and add to list
            List<ColorChip> inputChips = new List<ColorChip>();
            foreach (string chipStr in chipStrings) {
                string[] chipColors = Regex.Split(chipStr, @",\s*");
                if (chipColors.Length != 2) return null;
                
                if (Enum.TryParse(chipColors[0], true, out Color startColor) && Enum.TryParse(chipColors[1], true, out Color endColor)) {
                    inputChips.Add(new ColorChip(startColor, endColor));
                } else {
                    return null;
                }
            }
            return inputChips;
        }

        // method to initialize chips dictionary for faster read times when traversing the tree
        private static IDictionary<Color, List<Color>> initializeChipsDictionary(List<ColorChip> inputChips) {
            IDictionary<Color, List<Color>> chipsDict = new Dictionary<Color, List<Color>>();
            // prepopulate map with keys corresponding to each color and value as empty list as we only have a small finite set of colors
            foreach (Color color in Enum.GetValues(typeof(Color))) {
                chipsDict.Add(color, new List<Color>());
            }
            inputChips.ForEach(chip => chipsDict[chip.StartColor].Add(chip.EndColor));
            return chipsDict;
        }

        // method to recursively traverse through the "tree"-like structure of the chip connections
        private static List<ColorChip> searchTree(IDictionary<Color, List<Color>> chipsDict, Color startColor) {
            List<Color> endColors = chipsDict[startColor];

            // recursion stop conditions: last leaf in tree, no more possible connections after this color
            if (endColors.Count == 0) return null;

            // iterate through each sibling leaf of the tree and recursively check if they lead to FINAL_COLOR
            foreach (Color endColor in endColors) {
                // recursion stop conditions: last node contains FINAL_COLOR
                if (endColor == FINAL_COLOR) return new List<ColorChip> {new ColorChip(startColor, FINAL_COLOR)};

                List<ColorChip> result = searchTree(chipsDict, endColor);
                // if FINAL_COLOR was found, backtrack and add each node to the result list
                if (result != null && result.Count > 0) {
                    result.Insert(0, new ColorChip(startColor, endColor));
                    return result;
                }
            }

            return null;
        }

        // method to print the solution (or the lack thereof) to the console
        private static void printSolution(List<ColorChip> solution) {
            if (solution != null && solution.Count > 0) {
                Console.Write($"{SUCCESS_MSG}\n{INITIAL_COLOR} ");
                foreach (ColorChip chip in solution) {
                    Console.Write($"[{chip}] ");
                }
                Console.Write($"{FINAL_COLOR}\n");
            } else {
                Console.WriteLine(Constants.ErrorMessage);
            }
        }
    }
}
