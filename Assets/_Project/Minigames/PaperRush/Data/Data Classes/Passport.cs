using System;
using UnityEngine;

public class Passport: Document
{
    public string nationality;
    public string sex;
    public Texture2D photo;
    public DateTime dateOfBirth;
    public DateTime issueDate;
    public DateTime expiryDate;
    public int passportNumber;
}
