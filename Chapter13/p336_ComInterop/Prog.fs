#light
open SpeechLib

let voice = new SpVoiceClass()
voice.Speak("Hello world", SpeechVoiceSpeakFlags.SVSFDefault)
read_line()