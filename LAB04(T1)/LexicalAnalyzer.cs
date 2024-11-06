using System.Collections.Generic;
using System;

namespace LexicalAnalyzerApp
{
    public class LexicalAnalyzer
    {
        private string input;
        private int forward;
        private int lexemeStart;
        private char[] buffer1;
        private char[] buffer2;
        private bool useBuffer1;

        public LexicalAnalyzer(string sourceCode)
        {
            input = sourceCode;
            forward = 0;
            lexemeStart = 0;
            buffer1 = new char[10];
            buffer2 = new char[10];
            useBuffer1 = true;
            LoadBuffer();
        }

        public List<string> Analyze()
        {
            List<string> tokens = new List<string>();
            while (forward < input.Length)
            {
                char currentChar = GetNextChar();

                if (Char.IsLetter(currentChar))
                {
                    string token = GetIdentifier();
                    tokens.Add("IDENTIFIER: " + token);
                }
                else if (Char.IsDigit(currentChar))
                {
                    string token = GetNumber();
                    tokens.Add("NUMBER: " + token);
                }
                lexemeStart = forward;
            }

            return tokens;
        }

        private void LoadBuffer()
        {
            int start = forward;
            int end = Math.Min(forward + 10, input.Length);

            if (useBuffer1)
            {
                for (int i = 0; i < end - start; i++)
                    buffer1[i] = input[start + i];
            }
            else
            {
                for (int i = 0; i < end - start; i++)
                    buffer2[i] = input[start + i];
            }
        }

        private char GetNextChar()
        {
            if (useBuffer1)
            {
                if (forward % 10 == 0 && forward != lexemeStart)
                    useBuffer1 = false;
            }
            else
            {
                if (forward % 10 == 0 && forward != lexemeStart)
                    useBuffer1 = true;
            }

            char currentChar = useBuffer1 ? buffer1[forward % 10] : buffer2[forward % 10];
            forward++;

            if (forward % 10 == 0)
                LoadBuffer();

            return currentChar;
        }

        private string GetIdentifier()
        {
            string identifier = "";
            char currentChar = GetNextChar();
            while (Char.IsLetterOrDigit(currentChar))
            {
                identifier += currentChar;
                currentChar = GetNextChar();
            }
            forward--;
            return identifier;
        }

        private string GetNumber()
        {
            string number = "";
            char currentChar = GetNextChar();
            while (Char.IsDigit(currentChar))
            {
                number += currentChar;
                currentChar = GetNextChar();
            }
            forward--;
            return number;
        }
    }
}
