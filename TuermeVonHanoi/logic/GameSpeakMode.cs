﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace TuermeVonHanoi.logic
{
    class GameSpeakMode
    {

        private SpeechRecognitionEngine recognizer;

        static private Grammar grammar1 = new Grammar(@"Resources\grammar.xml", "Slot1");
        static private Grammar grammar2 = new Grammar(@"Resources\grammar.xml", "Slot2");
        static private Grammar grammar3 = new Grammar(@"Resources\grammar.xml", "Slot3");

        public event EventHandler ValueChanged;

        public GameSpeakMode()
        {

        }

        public void start()
        {
            this.recognizer = new SpeechRecognitionEngine();
            this.recognizer.SetInputToDefaultAudioDevice();

            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(grammar1);

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void stop()
        {
            recognizer.UnloadAllGrammars();
            recognizer.RecognizeAsyncCancel();
        }

        public void loadSlot1()
        {
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(grammar1);
            recognizer.RequestRecognizerUpdate();
        }

        public void loadSlot2()
        {
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(grammar2);
            recognizer.RequestRecognizerUpdate();
        }

        public void loadSlot3()
        {
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(grammar3);
            recognizer.RequestRecognizerUpdate();
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //foreach (RecognizedWordUnit word in e.Result.Words)
            //{
                // You can change the minimun confidence level here
                //if (word.Confidence > 0.1f)
                //System.Console.WriteLine(word.Text);
            //}

            //System.Console.WriteLine(e.Result.Semantics.Value);

            EventHandler handler = ValueChanged;
            if (handler != null) handler(this, e);
        }
    }
}
