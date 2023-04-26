using System;

[Serializable]
public class User
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}
 
[Serializable]
public class RootObject
{
    public User[] users; 
}

