#region p1q1
//Interface defines a contract that a class must implement. It defines what a class does but not how it does it.
//Interfaces are used instead of concrete classes to achieve loose coupling through abstraction which allows more flexibility , easy testing & maintainability
//achieve multiple inheritence - loose coupling - abstraction to define capability not implementation - polymorphism
#endregion

#region p1q2
//a
//same method name conflict - class currently provides same implementation for both 
//b
//Fix by explicit implementation - call through interface name
//class Translator : IEnglishSpeaker, IArabicSpeaker
//{
//    void IEnglishSpeaker.Greet()
//    {
//        Console.WriteLine("Hello");
//    }
//    void IArabicSpeaker.Greet()
//    {
//        Console.WriteLine("Ahlan");
//    }
//}
//c
//Greet() can only be called through interface reference pointing to Translator object only
//IEnglishSpeaker tEnglish = new Translator();
//IArabicSpeaker tArabic = new translator();
//tEnglish.Greet();
//tArabic.Greet();
#endregion

#region p1q3
//shallow copy 
//creates new object with same values but for reference types ref is copied - no new object
//deep copy
//creates new object with same values but for reference types new object is created and values are copied - no shared references
//So, if object is immutable shallow copy is fine but otherwise deep copy is used
//Risk with shallow copy with reference types is that changes are refleted in both objects -original and copy - due to shared reference
#endregion

#region p1q4
//Dev - Testing
//QA - Testing
//Title is string type - immutable - so each string is new object
//Dept is Department type - mutable - reference copied -  same object shared - changes reflect in both original and copy Employee
#endregion
