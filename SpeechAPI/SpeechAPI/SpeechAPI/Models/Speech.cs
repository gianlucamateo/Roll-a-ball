using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Speech.Recognition;

namespace SpeechAPI.Models
{
    public class Speech
    {
        public Queue<string> commands;
        public SpeechRecognitionEngine sr;
        public Speech()
        {
            commands = new Queue<string>();
            System.Diagnostics.Debug.WriteLine("called");
            buildSR();
        }

        public void buildSR()
        {
            sr = new SpeechRecognitionEngine();
            Choices colors = new Choices();
            colors.Add(new string[] { "Peter", "James", "Switch that"});

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance.
            Grammar g = new Grammar(gb);
            sr.LoadGrammar(g);

            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
            sr.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(sr_SpeechRejected);
            sr.SetInputToDefaultAudioDevice();
            sr.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.8)
            {
                commands.Enqueue(e.Result.Text);
            }
            System.Diagnostics.Debug.WriteLine(e.Result.Text + " confidence: " +e.Result.Confidence);            
                
        }
        void sr_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("failed");
        }
    }
}