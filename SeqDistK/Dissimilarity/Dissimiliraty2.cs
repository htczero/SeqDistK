using System;
using System.Collections.Generic;

namespace CalDistance
{
    class Dissimiliraty
    {
        private List<Action<SequenceData, SequenceData, Result, int, int>> _listFun = new List<Action<SequenceData, SequenceData, Result, int, int>>();

        public Dissimiliraty(List<string> listFunName)
        {
            List<string> listTmp = new List<string>();
            for (int i = 0; i < listFunName.Count; i++)
            {
                listTmp.Add(listFunName[i]);
            }
            if (listTmp.Contains("Eu") && listTmp.Contains("Ma") && listTmp.Contains("Ch") && listTmp.Contains("D2"))
            {
                listTmp.Remove("Eu");
                listTmp.Remove("Ma");
                listTmp.Remove("Ch");
                listTmp.Remove("D2");
                _listFun.Add(CalEuMaChD2);
            }
            if (listTmp.Contains("D2S") && listTmp.Contains("D2Star"))
            {
                listTmp.Remove("D2S");
                listTmp.Remove("D2Star");
                _listFun.Add(CalD2SD2Star);
            }
            foreach (var item in listTmp)
            {
                if (item == "Eu")
                    _listFun.Add(CalEu);
                else if (item == "Ma")
                    _listFun.Add(CalMa);
                else if (item == "Ch")
                    _listFun.Add(CalCh);
                else if (item == "D2")
                    _listFun.Add(CalD2);
                else if (item == "Hao")
                    _listFun.Add(CalHao);
                else if (item == "D2S")
                    _listFun.Add(CalD2S);
                else if (item == "D2Star")
                    _listFun.Add(CalD2Star);
            }
        }

        public void CalDissimiliraty(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            foreach (var cal in _listFun)
            {
                cal(seqX, seqY, result, k, m);
            }
        }

        private void CalHao(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            if (k < 2)
            {
                return;
            }
            double resultHao = 0;
            double tmpXY = 0;
            double tmpX = 0;
            double tmpY = 0;
            double tmp1 = 0;
            double tmp2 = 0;
            double fXi = 0;
            double fYi = 0;

            List<int> listKtupleX = seqX.GetKtupleData(k);
            List<double> listPosX = seqX.GetMarkovData(k, k - 2);
            int totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            List<double> listPosY = seqY.GetMarkovData(k, k - 2);
            int totalY = seqY.GetTotal(k);

            for (int i = 0; i < listKtupleY.Count; i++)
            {
                fXi = listKtupleX[i] * 1.0 / totalX;
                fYi = listKtupleY[i] * 1.0 / totalY;
                if (listPosX[i] == 0)
                    tmp1 = -1;
                else
                    tmp1 = (fXi / listPosX[i]) - 1;
                if (listPosY[i] == 0)
                    tmp2 = -1;
                else
                    tmp2 = (fYi / listPosY[i]) - 1;
                tmpXY += tmp1 * tmp2;
                tmpX += tmp1 * tmp1;
                tmpY += tmp2 * tmp2;
            }
            tmpX = Math.Sqrt(tmpX);
            tmpY = Math.Sqrt(tmpY);
            resultHao = (1 - tmpXY / (tmpX * tmpY)) / 2;
            SaveResult("Hao_k" + k, seqX, seqY, result, resultHao);
            //result.AddToMatrix("Hao_k" + k, seqX.ID, seqY.ID, resultHao);
            //result.AddToMatrix("Hao_k" + k, seqY.ID, seqX.ID, resultHao);

        }

        private void CalEuMaChD2(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            //Eu
            double resultEu = 0;
            double tmpEu = 0;

            //Ma
            double tmpMa = 0;
            double resultMa = 0;

            //public
            double fXi = 0;
            double fYi = 0;
            double cXi = 0;
            double cYi = 0;

            //Ch
            double resultCh = 0;

            //D2
            double resultD2 = 0;
            double tmpXD2 = 0;
            double tmpYD2 = 0;
            double tmpD2 = 0;

            List<int> listKtupleX = seqX.GetKtupleData(k);
            int totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            int totalY = seqY.GetTotal(k);

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                cXi = listKtupleX[i];
                cYi = listKtupleY[i];
                fXi = cXi / totalX;
                fYi = cYi / totalY;

                //D2
                tmpD2 += cYi * cXi;
                tmpXD2 += cXi * cXi;
                tmpYD2 += cYi * cYi;

                //Eu
                tmpEu = (fXi - fYi) * (fXi - fYi);
                resultEu += tmpEu;

                //Ma
                tmpMa = Math.Abs(fXi - fYi);
                resultMa += tmpMa;

                //Ch
                resultCh = tmpMa > resultCh ? tmpMa : resultCh;
            }
            //D2
            tmpXD2 = Math.Sqrt(tmpXD2);
            tmpYD2 = Math.Sqrt(tmpYD2);
            tmpD2 = tmpD2 / (tmpXD2 * tmpYD2);
            resultD2 = 0.5 * (1 - tmpD2);

            //Eu
            resultEu = Math.Sqrt(resultEu);

            SaveResult("Eu_k" + k, seqX, seqY, result, resultEu);
            SaveResult("Ch_k" + k, seqX, seqY, result, resultCh);
            SaveResult("Ma_k" + k, seqX, seqY, result, resultMa);
            SaveResult("D2_k" + k, seqX, seqY, result, resultD2);

            //result.AddToMatrix("Eu_k" + k, seqX.ID, seqY.ID, resultEu);
            //result.AddToMatrix("Ch_k" + k, seqX.ID, seqY.ID, resultCh);
            //result.AddToMatrix("Ma_k" + k, seqX.ID, seqY.ID, resultMa);
            //result.AddToMatrix("D2_k" + k, seqX.ID, seqY.ID, resultD2);

            //result.AddToMatrix("Eu_k" + k, seqY.ID, seqX.ID, resultEu);
            //result.AddToMatrix("Ch_k" + k, seqY.ID, seqX.ID, resultCh);
            //result.AddToMatrix("Ma_k" + k, seqY.ID, seqX.ID, resultMa);
            //result.AddToMatrix("D2_k" + k, seqY.ID, seqX.ID, resultD2);



        }

        private void CalD2(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            //public
            double cXi = 0;
            double cYi = 0;

            //D2
            double resultD2 = 0;
            double tmpXD2 = 0;
            double tmpYD2 = 0;
            double tmpD2 = 0;

            List<int> listKtupleX = seqX.GetKtupleData(k);
            List<int> listKtupleY = seqY.GetKtupleData(k);

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                cXi = listKtupleX[i];
                cYi = listKtupleY[i];

                //D2
                tmpD2 += cYi * cXi;
                tmpXD2 += cXi * cXi;
                tmpYD2 += cYi * cYi;
            }
            //D2
            tmpXD2 = Math.Sqrt(tmpXD2);
            tmpYD2 = Math.Sqrt(tmpYD2);
            tmpD2 = tmpD2 / (tmpXD2 * tmpYD2);
            resultD2 = 0.5 * (1 - tmpD2);

            SaveResult("D2_k" + k, seqX, seqY, result, resultD2);

            //result.AddToMatrix("D2_k" + k, seqX.ID, seqY.ID, resultD2);
            //result.AddToMatrix("D2_k" + k, seqY.ID, seqX.ID, resultD2);
        }

        private void CalCh(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            //public
            double fXi = 0;
            double fYi = 0;
            double cXi = 0;
            double cYi = 0;

            //Ch
            double resultCh = 0;
            double tmpCh = 0;

            List<int> listKtupleX = seqX.GetKtupleData(k);
            int totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            int totalY = seqY.GetTotal(k);

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                cXi = listKtupleX[i];
                cYi = listKtupleY[i];
                fXi = cXi / totalX;
                fYi = cYi / totalY;

                //Ch
                tmpCh = Math.Abs(fXi - fYi);
                resultCh = tmpCh > resultCh ? tmpCh : resultCh;
            }
            //Ch
            SaveResult("Ch_k" + k, seqX, seqY, result, resultCh);
            //result.AddToMatrix("Ch_k" + k, seqX.ID, seqY.ID, resultCh);
            //result.AddToMatrix("Ch_k" + k, seqY.ID, seqX.ID, resultCh);
        }

        private void CalEu(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            //public
            double fXi = 0;
            double fYi = 0;
            double cXi = 0;
            double cYi = 0;

            //Eu
            double resultEu = 0;
            double tmpEu = 0;


            List<int> listKtupleX = seqX.GetKtupleData(k);
            int totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            int totalY = seqY.GetTotal(k);

            //Eu
            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                cXi = listKtupleX[i];
                cYi = listKtupleY[i];
                fXi = cXi / totalX;
                fYi = cYi / totalY;

                //Eu
                tmpEu = (fXi - fYi) * (fXi - fYi);
                resultEu += tmpEu;
            }
            //Eu
            resultEu = Math.Sqrt(resultEu);
            SaveResult("Eu_k" + k, seqX, seqY, result, resultEu);

            //result.AddToMatrix("Eu_k" + k, seqX.ID, seqY.ID, resultEu);
            //result.AddToMatrix("Eu_k" + k, seqY.ID, seqX.ID, resultEu);
        }

        private void CalMa(SequenceData seqX, SequenceData seqY, Result result, int k, int m = 0)
        {
            //public
            double fXi = 0;
            double fYi = 0;
            double cXi = 0;
            double cYi = 0;

            //Ma
            double tmpMa = 0;
            double resultMa = 0;


            List<int> listKtupleX = seqX.GetKtupleData(k);
            int totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            int totalY = seqY.GetTotal(k);

            //Ma
            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                cXi = listKtupleX[i];
                cYi = listKtupleY[i];
                fXi = cXi / totalX;
                fYi = cYi / totalY;

                //Ma
                tmpMa = Math.Abs(fXi - fYi);
                resultMa += tmpMa;
            }
            //Ma
            SaveResult("Ma_k" + k, seqX, seqY, result, resultMa);

            //result.AddToMatrix("Ma_k" + k, seqX.ID, seqY.ID, resultMa);
            //result.AddToMatrix("Ma_k" + k, seqY.ID, seqX.ID, resultMa);
        }

        private void CalD2S(SequenceData seqX, SequenceData seqY, Result result, int k, int m)
        {
            if (k < 3)
            {
                return;
            }
            //public
            List<int> listKtupleX = seqX.GetKtupleData(k);
            List<double> listPosX = seqX.GetMarkovData(k, m);
            double totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            List<double> listPosY = seqY.GetMarkovData(k, m);
            double totalY = seqY.GetTotal(k);


            //D2S
            double resultD2S = 0;
            double tmpD2S = 0;
            double tmpXD2S = 0;
            double tmpYD2S = 0;

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                double cXi = listKtupleX[i];
                double cYi = listKtupleY[i];
                double pXi = listPosX[i];
                double pYi = listPosY[i];
                double tmpX = totalX * pXi;
                double tmpY = totalY * pYi;
                double cXi_bar = cXi - tmpX;
                double cYi_bar = cYi - tmpY;
                double tmp1 = cXi_bar * cXi_bar;
                double tmp2 = cYi_bar * cYi_bar;



                tmpD2S = Math.Sqrt(tmp1 + tmp2);
                if (tmpD2S == 0)
                    tmpD2S = 1;
                resultD2S += cXi_bar * cYi_bar / tmpD2S;
                tmpXD2S += tmp1 / tmpD2S;
                tmpYD2S += tmp2 / tmpD2S;

            }

            tmpXD2S = Math.Sqrt(tmpXD2S);
            tmpYD2S = Math.Sqrt(tmpYD2S);
            resultD2S = (1 - resultD2S / (tmpXD2S * tmpYD2S)) * 0.5;

            SaveResult("D2S_k" + k + "_M" + m, seqX, seqY, result, resultD2S);

            //result.AddToMatrix("D2S_k" + k + "_M" + m, seqX.ID, seqY.ID, resultD2S);
            //result.AddToMatrix("D2S_k" + k + "_M" + m, seqY.ID, seqX.ID, resultD2S);
        }

        private void CalD2Star(SequenceData seqX, SequenceData seqY, Result result, int k, int m)
        {
            if (k < 3)
            {
                return;
            }
            //public
            List<int> listKtupleX = seqX.GetKtupleData(k);
            List<double> listPosX = seqX.GetMarkovData(k, m);
            double totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            List<double> listPosY = seqY.GetMarkovData(k, m);
            double totalY = seqY.GetTotal(k);

            //D2Star
            double tmpXD2Star = 0;
            double tmpYD2Star = 0;
            double resultD2Star = 0;

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                double cXi = listKtupleX[i];
                double cYi = listKtupleY[i];
                double pXi = listPosX[i];
                double pYi = listPosY[i];
                double tmpX = totalX * pXi;
                double tmpY = totalY * pYi;
                double cXi_bar = cXi - tmpX;
                double cYi_bar = cYi - tmpY;
                double tmp1 = cXi_bar * cXi_bar;
                double tmp2 = cYi_bar * cYi_bar;


                double tmp3 = Math.Sqrt(tmpX * tmpY);
                if (tmpX == 0)
                {
                    tmpX = tmp3 = 1;
                }
                if (tmpY == 0)
                {
                    tmpX = tmp3 = 1;
                }
                resultD2Star += cXi_bar * cYi_bar / tmp3;
                tmpXD2Star += tmp1 / tmpX;
                tmpYD2Star += tmp2 / tmpY;

            }

            //D2Star

            tmpXD2Star = Math.Sqrt(tmpXD2Star);
            tmpYD2Star = Math.Sqrt(tmpYD2Star);
            resultD2Star = 0.5 * (1 - resultD2Star / (tmpXD2Star * tmpYD2Star));
            SaveResult("D2Star_k" + k + "_M" + m, seqX, seqY, result, resultD2Star);
            //result.AddToMatrix("D2Star_k" + k + "_M" + m, seqX.ID, seqY.ID, resultD2Star);
            //result.AddToMatrix("D2Star_k" + k + "_M" + m, seqY.ID, seqX.ID, resultD2Star);
        }

        private void CalD2SD2Star(SequenceData seqX, SequenceData seqY, Result result, int k, int m)
        {
            if (k < 3 || k == m)
            {
                return;
            }
            //public
            List<int> listKtupleX = seqX.GetKtupleData(k);
            List<double> listPosX = seqX.GetMarkovData(k, m);
            double totalX = seqX.GetTotal(k);

            List<int> listKtupleY = seqY.GetKtupleData(k);
            List<double> listPosY = seqY.GetMarkovData(k, m);
            double totalY = seqY.GetTotal(k);

            //D2S
            double resultD2S = 0;
            double tmpD2S = 0;
            double tmpXD2S = 0;
            double tmpYD2S = 0;

            //D2Star
            double tmpXD2Star = 0;
            double tmpYD2Star = 0;
            double resultD2Star = 0;

            for (int i = 0; i < listKtupleX.Count; i++)
            {
                //public
                double cXi = listKtupleX[i];
                double cYi = listKtupleY[i];
                double pXi = listPosX[i];
                double pYi = listPosY[i];
                double tmpX = totalX * pXi;
                double tmpY = totalY * pYi;
                double cXi_bar = cXi - tmpX;
                double cYi_bar = cYi - tmpY;
                double tmp1 = cXi_bar * cXi_bar;
                double tmp2 = cYi_bar * cYi_bar;


                //D2S
                tmpD2S = Math.Sqrt(tmp1 + tmp2);
                if (tmpD2S == 0)
                    tmpD2S = 1;

                resultD2S += cXi_bar * cYi_bar / tmpD2S;
                tmpXD2S += tmp1 / tmpD2S;
                tmpYD2S += tmp2 / tmpD2S;


                //D2Star
                double tmp3 = Math.Sqrt(tmpX * tmpY);
                if (tmpX == 0)
                {
                    tmpX = tmp3 = 1;
                }
                if (tmpY == 0)
                {
                    tmpY = tmp3 = 1;
                }
                resultD2Star += cXi_bar * cYi_bar / tmp3;
                tmpXD2Star += tmp1 / tmpX;
                tmpYD2Star += tmp2 / tmpY;

            }

            tmpXD2S = Math.Sqrt(tmpXD2S);
            tmpYD2S = Math.Sqrt(tmpYD2S);
            resultD2S = (1 - resultD2S / (tmpXD2S * tmpYD2S)) * 0.5;
            SaveResult("D2S_k" + k + "_M" + m, seqX, seqY, result, resultD2S);
            //result.AddToMatrix("D2S_k" + k + "_M" + m, seqX.ID, seqY.ID, resultD2S);
            //result.AddToMatrix("D2S_k" + k + "_M" + m, seqY.ID, seqX.ID, resultD2S);

            //D2Star

            tmpXD2Star = Math.Sqrt(tmpXD2Star);
            tmpYD2Star = Math.Sqrt(tmpYD2Star);
            resultD2Star = 0.5 * (1 - resultD2Star / (tmpXD2Star * tmpYD2Star));
            SaveResult("D2Star_k" + k + "_M" + m, seqX, seqY, result, resultD2Star);
            //result.AddToMatrix("D2Star_k" + k + "_M" + m, seqX.ID, seqY.ID, resultD2Star);
            //result.AddToMatrix("D2Star_k" + k + "_M" + m, seqY.ID, seqX.ID, resultD2Star);
        }

        private void SaveResult(string key, SequenceData seqX, SequenceData seqY, Result result, double value)
        {
            result.Add(key, seqX.ID, seqY.ID, value);
        }
    }
}
