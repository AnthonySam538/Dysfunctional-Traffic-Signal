// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #1
// Program Name: Dysfunctional Traffic Signal

//Name of this file: DysfunctionalMain.cs
//Purpose of this file: Launch the window showing the form where the dysfunctional light will be displayed.
//Purpose of this entire program: This program shows a blinking red light. This program contains exactly one clock.

//Source files in this program: DysfunctionalForm.cs, DysfunctionalMain.cs
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. DysfunctionalForm.cs compiles into library file DysfunctionalForm.dll
//  2. DysfunctionalMain.cs compiles and links with the dll file above to create Dysfunctional.exe
//Compile (and link) this file:
//mcs -r:System.Windows.Forms.dll -r:DysfunctionalForm.dll -out:Dysfunctional.exe DysfunctionalMain.cs
//Execute (Linux shell): ./Dysfunctional.exe

using System;
using System.Windows.Forms;  //Needed for "Application.Run" near the end of Main function.

public class DysfunctionalMain
{
  public static void Main()
  {
    System.Console.WriteLine("The blinking red light program will begin now.");
    DysfunctionalForm Dysfunctional_App = new DysfunctionalForm();
    Application.Run(Dysfunctional_App);
    System.Console.WriteLine("The blinking red light program has ended. Bye.");
  }
}
