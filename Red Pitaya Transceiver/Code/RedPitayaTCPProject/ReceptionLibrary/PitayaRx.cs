using System;
using System.Collections.Generic;

using namespace ReceptionLibrary 
{
  public abstract class PitayaRx
  {
  public Rxtype type;
  public double freq;
  public double getStrength()
    {
      return getStrength(freq);
    }
  public abstract double getStrength(double freq);
  public void addStationBookmark()
    {
      addStationBookmark(freq);
     }
  public abstract void addStationBookmark(double freq);
  public abstract list<double> getScan (double threshold); 
 }
 public enum Rxtype { narrowFM = 0, AM = 1, SSBAM = 2}
 
 
 
 }
 
