using System;

namespace ONP
{
	public class IncorrectOperationStringException : Exception
    {
		//dlaczego obciążać klienta odpowiedzialnością wypisania całej wiadomości?
		//niech Twój dedykowany wyjątek przyjmuje parametry i sam zatroszczy się o komunikat
		//to jego odpowiedzialność!
        public IncorrectOperationStringException(string operation)
        : base("Incorrect operation string: " + operation)
        {
        }
    }
}
