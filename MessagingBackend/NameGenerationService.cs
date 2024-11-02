using System.Collections.Generic;
using System;

namespace MessagingBackend
{
    public static class NameGenerationService
    {
        static readonly List<string> quirkyAdjectives = 
        [
            "Ditzy",
            "Funky",
            "Clumsy",
            "Stupid",
            "Whimsical",
            "Zany",
            "Eccentric",
            "Quirky",
            "Kooky",
            "Bizarre",
            "Peculiar",
            "Offbeat",
            "Wacky",
            "Quirk-tastic",
            "Silly",
            "Fanciful",
            "Nonsensical",
            "Playful",
            "Zesty",
            "Spunky",
            "Fiddly",
            "Droll",
            "Dizzying",
            "Jolly",
            "Snazzy",
            "Groovy",
            "Spiffy",
            "Frothy",
            "Perky",
            "Lively",
            "Jubilant",
            "Zippy",
            "Merry",
            "Gleeful",
            "Kinky",
            "Pervy",
            "Sexy"
        ];

        static readonly List<string> animalNames = 
        [
            "LION",
            "TIGER",
            "ELEPHANT",
            "KANGAROO",
            "GIRAFFE",
            "FLAMINGO",
            "OSTRICH",
            "HEDGEHOG",
            "PANDA",
            "DOLPHIN",
            "ZEBRA",
            "PEACOCK",
            "SLOTH",
            "RHINOCEROS",
            "CROCODILE",
            "KOALA",
            "MEERKAT",
            "CAPYBARA",
            "TORTOISE",
            "PENGUIN",
            "SQUIRREL",
            "OCTOPUS",
            "CHAMELEON",
            "WALRUS",
            "BISON",
            "ALPACA",
            "SEA URCHIN",
            "VULTURE",
            "MANATEE",
            "GOLDEN RETRIEVER"
        ];

        public static string GetUniqueSocketId() {
            var random = new Random();
            return $"{quirkyAdjectives[random.Next(quirkyAdjectives.Count)]} {animalNames[random.Next(animalNames.Count)]}";
        }
    }
}
