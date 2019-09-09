// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #2
// Program Name: Traffic Signal
//
// Name of this file: TrafficSignalMain.cs
// Purpose of this file: Launch the form where the traffic signal will be displayed.
// Purpose of this entire program: Display a traffic signal. This program contains exactly one clock.
// Source files in this program: TrafficSignalForm.cs, TrafficSignalMain.cs

using System;
using System.Windows.Forms;

public class TrafficSignalMain
{
  public static void Main()
  {
    System.Console.WriteLine("The traffic signal program will begin now.");
    TrafficSignalForm TrafficSignal_App = new TrafficSignalForm();
    Application.Run(TrafficSignal_App);
    System.Console.WriteLine("The traffic signal program has ended.");
  }
}
