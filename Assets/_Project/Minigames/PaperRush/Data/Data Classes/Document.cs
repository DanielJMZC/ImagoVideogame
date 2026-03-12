using System.Collections.Generic;
using UnityEngine;

public enum documentError
{
    ErrorInFieldOne, ErrorInFieldTwo, ErrorInFieldThree, MismatchDocument, NoDocument, None
}
public class Document
{
    public string firstNames;
    public string lastNames;
    public documentType type;
    public documentError errorType = documentError.NoDocument;

    public List<string> documentErrors = new List<string>();

 
}
