using System;

namespace TransmissionLibrary
{
    public enum TransmissionType { narrowFM = 0, AM, SSBAM}

    public abstract class PitayaTx
    {
        private double baseFreq;
        private double freq;
        private TransmissionType txType;

        public double BaseFreq { get => baseFreq; set => baseFreq = value; }
        public double Freq { get => freq; set => freq = value; }
        public TransmissionType TxType { get => txType; set => txType = value; }

        public bool Tx()
        {
            return Tx(baseFreq);
        }

        public abstract void SetFrequency(double freq);
        public abstract bool RecallBookmark(long id);
        public abstract bool Tx(double baseFreq);
    }
}
