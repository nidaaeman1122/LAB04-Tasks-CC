using System;
using System.Collections.Generic;

public class LexicalAnalyzer
{
    private string input;   // Source code to analyze
    private int forward;    // Pointer for forward scanning
    private int lexemeStart;  // Pointer for the start of the lexeme
    private char[] buffer1;  // First buffer
    private char[] buffer2;  // Second buffer
    private bool useBuffer1; // Switch between buffers

    public LexicalAnalyzer(string sourceCode)
    {
        input = sourceCode;
        forward = 0;
        lexemeStart = 0;
        buffer1 = new char[10];  // Define buffer size, e.g., 10 characters
        buffer2 = new char[10];
        useBuffer1 = true;
        LoadBuffer();  // Initially load the first buffer
    }

    // Method to tokenize the input
    public List<string> Analyze()
    {
        List<string> tokens = new List<string>();
        while (forward < input.Length)
        {
            char currentChar = GetNextChar();

            if (Char.IsLetter(currentChar))  // If it's an identifier
            {
                string token = GetIdentifier();
                tokens.Add("IDENTIFIER: " + token);
            }
            else if (Char.IsDigit(currentChar))  // If it's a digit
            {
                string token = GetNumber();
                tokens.Add("NUMBER: " + token);
            }
            // Add more rules here for operators, etc.

            lexemeStart = forward;  // Move to the next lexeme
        }

        return tokens;
    }

    // Method to load the next buffer
    private void LoadBuffer()
    {
        int start = forward;
        int end = Math.Min(forward + 10, input.Length); // 10 is the buffer size

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

    // Method to get the next character and switch buffers if needed
    private char GetNextChar()
    {
        if (useBuffer1)
        {
            if (forward % 10 == 0 && forward != lexemeStart)  // Reached the end of the buffer
                useBuffer1 = false;
        }
        else
        {
            if (forward % 10 == 0 && forward != lexemeStart)
                useBuffer1 = true;
        }

        char currentChar = useBuffer1 ? buffer1[forward % 10] : buffer2[forward % 10];
        forward++;

        if (forward % 10 == 0)  // Load next buffer when crossing buffer boundary
            LoadBuffer();

        return currentChar;
    }

    // Methods to get identifiers and numbers
    private string GetIdentifier()
    {
        string identifier = "";
        char currentChar = GetNextChar();
        while (Char.IsLetterOrDigit(currentChar))
        {
            identifier += currentChar;
            currentChar = GetNextChar();
        }
        forward--;  // Adjust pointer to handle non-identifier characters
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
        forward--;  // Adjust pointer
        return number;
    }
}
