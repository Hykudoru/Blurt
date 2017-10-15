using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace Blurt_Reader
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer voiceBox;
        private string text;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            text = String.Empty;

            voiceBox = new SpeechSynthesizer()
            {
                Rate = trackBarSpeed.Value,
                Volume = trackBarVolume.Value
            };

            voiceBox.SpeakProgress += OnWordSpoken;
            voiceBox.SpeakStarted += OnStartedSpeaking;
        }

        private void OnWordSpoken(object sender, SpeakProgressEventArgs e)
        {
            textBox2.AppendText(e.Text + " ");
        }

        private void OnStartedSpeaking(object sender, SpeakStartedEventArgs e)
        {
            textBox2.Text = String.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelVolume_Click(object sender, EventArgs e)
        {

        }

        private void buttonSpeak_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                if (voiceBox.State == SynthesizerState.Paused)
                {
                    voiceBox.Resume();
                }
                //else if (voiceBox.GetCurrentlySpokenPrompt() != null)
                else if (voiceBox.GetCurrentlySpokenPrompt() == null)
                {
                    //voiceBox.SpeakAsyncCancel(voiceBox.GetCurrentlySpokenPrompt());
                    text = textBox1.Text;

                    if (checkBoxReverse.Checked)
                    {
                        char[] charSequence = text.ToCharArray();
                        text = new string(charSequence.Reverse().ToArray<char>());
                    }

                    voiceBox.SpeakAsync(text);
                }
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (voiceBox.State == SynthesizerState.Speaking)
            {
                voiceBox.Pause();
            }
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            if (voiceBox.State == SynthesizerState.Paused)
            {
                voiceBox.Resume();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (voiceBox.GetCurrentlySpokenPrompt() != null)
            {
                voiceBox.SpeakAsyncCancel(voiceBox.GetCurrentlySpokenPrompt());
            }
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            voiceBox.Volume = trackBarVolume.Value;
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            voiceBox.Rate = trackBarSpeed.Value;
        }

        private void radioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {
            voiceBox.SelectVoiceByHints(VoiceGender.Female);
        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {
            voiceBox.SelectVoiceByHints(VoiceGender.Male);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxReverse_CheckedChanged(object sender, EventArgs e)
        {
            text.Reverse();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
