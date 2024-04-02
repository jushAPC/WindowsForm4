using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cusipag_TP
{
    public partial class Form1 : Form
    {

        private string QItem1, QItem2, LItem1, LItem2, LItem3, CItem1, CItem2, CItem3, EItem1, QScore1, QScore2, LScore1, LScore2, LScore3, CScore1, CScore2, CScore3, EScore1, remarks;

       

        private double ComputedPeriodicGrade, EquivalentGrade;


        public Form1()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            
                   
                  

                    ScoreValidator scoreV = new ScoreValidator();
                    try
                    {





                        int quiz1items = scoreV.ValidateScore(QScore1);
                        int quiz2items = scoreV.ValidateScore(QScore2);
                        int lab1items = scoreV.ValidateScore(LScore1);
                        int lab2items = scoreV.ValidateScore(LScore2);
                        int lab3items = scoreV.ValidateScore(LScore3);
                        int act1items = scoreV.ValidateScore(CScore1);
                        int act2items = scoreV.ValidateScore(CScore2);
                        int act3items = scoreV.ValidateScore(CScore3);
                        int examScores = scoreV.ValidateScore(EScore1);

                        int quiz1Total = int.Parse(QItem1);
                        int quiz2Total = int.Parse(QItem2);
                        int lab1Total = int.Parse(LItem1);
                        int lab2Total = int.Parse(LItem2);
                        int lab3Total = int.Parse(LItem3);
                        int act1Total = int.Parse(CItem1);
                        int act2Total = int.Parse(CItem2);
                        int act3Total = int.Parse(CItem3);
                        int examTotal = int.Parse(EItem1);

                        scoreV.CheckValidScore(quiz1items, quiz1Total);
                        scoreV.CheckValidScore(quiz2items, quiz2Total);
                        scoreV.ValidateQuizScore(quiz1Total + quiz2Total);
                        scoreV.CheckValidScore(lab1items, lab1Total);
                        scoreV.CheckValidScore(lab2items, lab2Total);
                        scoreV.CheckValidScore(lab3items, lab3Total);
                        scoreV.ValidateLaboratoryActivityScore(lab1Total + lab2Total + lab3Total);
                        scoreV.CheckValidScore(act1items, act1Total);
                        scoreV.CheckValidScore(act2items, act2Total);
                        scoreV.CheckValidScore(act3items, act3Total);
                        scoreV.ValidateClassroomActivityScore(act1Total + act2Total + act3Total);
                        scoreV.CheckValidScore(examScores, examTotal);
                        scoreV.ValidateExamScore(examTotal);

                        GradeCalculator calculator = new GradeCalculator();

                        ComputedPeriodicGrade = calculator.ComputePeriodicGrade(quiz1items, quiz2items, quiz1Total, quiz2Total, lab1items, lab2items, lab3items, lab1Total,
                                                                                lab2Total, lab3Total,act1items, act2items, act3items, act1Total, act2Total, act3Total, examScores, examTotal);
                                                                        


                        EquivalentGrade = calculator.ComputeSubjectGrade(ComputedPeriodicGrade);

                        remarks = calculator.ComputePassFail(ComputedPeriodicGrade);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    grade.Text = $"{ComputedPeriodicGrade}";
                    equiV.Text = $"{EquivalentGrade}";
                    rems.Text = $"{remarks}";
                    






             
            

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {

            QItem1 = QItem1_Textbox.Text;
            QItem2 = QItem2_Textbox.Text;
            QScore1 = QScore1_Textbox.Text;
            QScore2 = QScore2_Textbox.Text;
            LItem1 = LItem1_Textbox.Text;
            LItem2 = LItem2_Textbox.Text;
            LItem3 = LItem3_Textbox.Text;
            LScore1 = LScore1_Textbox.Text;
            LScore2 = LScore2_Textbox.Text;
            LScore3 = LScore3_Textbox.Text;
            CItem1 = CItem1_Textbox.Text;
            CItem2 = CItem2_Textbox.Text;
            CItem3 = CItem3_Textbox.Text;
            CScore1 = CScore1_Textbox.Text;
            CScore2 = CScore2_Textbox.Text;
            CScore3 = CScore3_Textbox.Text;
            EItem1 = EItem1_Textbox.Text;
            EScore1 = EScore1_Textbox.Text;
            grade.Text = $"{ComputedPeriodicGrade}";
            equiV.Text = $"{EquivalentGrade}";
            rems.Text = $"{remarks}";

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            QItem1_Textbox.Clear()  ;
            QItem2_Textbox.Clear()  ;
            QScore1_Textbox.Clear()  ;
            QScore2_Textbox.Clear()  ;
            LItem1_Textbox.Clear()  ;
            LItem2_Textbox.Clear()  ;
            LItem3_Textbox.Clear()  ;
            LScore1_Textbox.Clear()  ;
            LScore2_Textbox.Clear()  ;
            LScore3_Textbox.Clear()  ;
            CItem1_Textbox.Clear()  ;
            CItem2_Textbox.Clear()  ;
            CItem3_Textbox.Clear()  ;
            CScore1_Textbox.Clear()  ;
            CScore2_Textbox.Clear()  ;
            CScore3_Textbox.Clear()  ;
            EItem1_Textbox.Clear()  ;
            EScore1_Textbox.Clear()  ;

        }
    }



    public class GradeCalculator
    {
        public double CalculateQuiz1Score(double quiz1, int quiz1items)
        {
            return Math.Round((quiz1 / quiz1items) * 50 + 50);
        }

        public double CalculateQuiz2Score(double quiz2, int quiz2items)
        {
            return Math.Round((quiz2 / quiz2items) * 50 + 50, 3);
        }

        public double CalculateQuizComponent(double quiz1score, double quiz2score)
        {
            return Math.Round(((quiz1score + quiz2score) / 2) * 0.1, 3);
        }

        public double CalculateLabScore(double lab1, double lab2, double lab3, int lab1items, int lab2items, int lab3items)
        {
            return Math.Round(((lab1 + lab2 + lab3) / (lab1items + lab2items + lab3items)) * 50 + 50);
        }

        public double CalculateLabComponent(double labScore)
        {
            return Math.Round(labScore * 0.5, 1);
        }

        public double CalculateActivityScore(double act1, double act2, double act3, int act1items, int act2items, int act3items)
        {
            return Math.Round(((act1 + act2 + act3) / (act1items + act2items + act3items)) * 50 + 50, 3);
        }

        public double CalculateActivityComponent(double actScore)
        {
            return Math.Round(actScore * 0.1, 3);
        }

        public double CalculateExamScore(double exam, int examItems)
        {
            return Math.Round(exam / examItems * 50 + 50);
        }

        public double CalculateExamComponent(double examScore)
        {
            return Math.Round(examScore * 0.3, 3);
        }

        public double CalculateTotalGrade(double quizComp, double labComp, double actComp, double examComp)
        {
            double periodicGrade = quizComp + labComp + actComp + examComp;
            return periodicGrade * 3;
        }

        public double ComputePeriodicGrade(double quiz1, double quiz2, int quiz1Items, int quiz2Items,
                                                  double lab1, double lab2, double lab3, int lab1Items, int lab2Items, int lab3Items,
                                                  double act1, double act2, double act3, int act1Items, int act2Items, int act3Items,
                                                  double exam, int examItems)
        {
            double quizComponent = CalculateQuizComponent(CalculateQuiz1Score(quiz1, quiz1Items), CalculateQuiz2Score(quiz2, quiz2Items));
            double labComponent = CalculateLabComponent(CalculateLabScore(lab1, lab2, lab3, lab1Items, lab2Items, lab3Items));
            double activityComponent = CalculateActivityComponent(CalculateActivityScore(act1, act2, act3, act1Items, act2Items, act3Items));
            double examComponent = CalculateExamComponent(CalculateExamScore(exam, examItems));
            double periodicGrade = quizComponent + labComponent + activityComponent + examComponent;
            return periodicGrade;
        }




        
        
            public double ComputeSubjectGrade(double grade)
            {
                
                if (grade >= 97.5)
                {
                    return  1.0;
                }
                else if (grade >= 94.5)
                {
                    return  1.25;
                }
                else if (grade >= 91.5)
                {
                    return  1.5;
                }
                else if (grade >= 88.5)
                {
                    return  1.75;
                }
                else if (grade >= 85.5)
                {
                    return  2.0;
                }
                else if (grade >= 82.5)
                {
                    return  2.25;
                }
                else if (grade >= 79.5)
                {
                    return  2.5;
                }
                else if (grade >= 76.5)
                {
                    return  2.75;
                }
                else if (grade >= 74.5)
                {
                    return  3.0;
                }
                else
                {
                   return 5.0;
                }

            }

            public string ComputePassFail(double periodicGrade)
            {
                return periodicGrade > 74.4 ? "Passed" : "Failed";
            }
        }



        public class ScoreValidator
        {
            public void ValidateExamScore(int score)
            {
                if (score < 0 || score > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(score), "Exam score must be between 0 and 100.");
                }
            }
            public void ValidateQuizScore(int score)
            {
                if (score < 0 || score > 50)
                {
                    throw new ArgumentOutOfRangeException(nameof(score), "Quiz score must be between 0 and 50.");
                }
            }
            public void ValidateClassroomActivityScore(int score)
            {
                if (score < 0 || score > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(score), "Classroom activity score must be between 0 and 100.");
                }
            }
            public void ValidateLaboratoryActivityScore(int score)
            {
                if (score < 0 || score > 100)
                {
                    throw appropriateException(score, 50, 100, "Laboratory activity score");
                }
            }

            public void CheckValidScore(int score, int total)
            {
                 if (score < 0 || score > total)
                {
                    throw new ArgumentOutOfRangeException("Score can't be less than zero or exceed total items.");
                } 
            }

            public int ValidateScore(string score)
            {
                if (string.IsNullOrWhiteSpace(score))
                {
                    return 0;
                }

                foreach (char c in score)

                {

                    if (!char.IsDigit(c))

                    {
                        throw new ArgumentException("Alphanumeric text is considered invalid input.");
                    }
                }
                return int.Parse(score);

            }

            private Exception appropriateException(int score, int min, int max, string scoreType)
            {
                if (score < min)
                {
                    return new ArgumentOutOfRangeException(nameof(score), $"{scoreType} score must be between {min} and {max}.");
                }
                else
                {
                    return new ArgumentOutOfRangeException(nameof(score), $"{scoreType} score cannot exceed {max}.");
                }
            }
        }

    }
