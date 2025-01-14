﻿/*
Copyright 2015-2022 National Technology & Engineering Solutions of Sandia, LLC (NTESS).
Under the terms of Contract DE-NA0003525 with NTESS, the U.S.Government retains certain
rights in this software.

You should have received a copy of the GNU General Public License along with
HyRAM+. If not, see https://www.gnu.org/licenses/.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SandiaNationalLaboratories.Hyram
{
    public partial class UnconfinedOverpressureForm : UserControl
    {
        private readonly StateContainer _state = State.Data;
        // Results
        private bool _analysisStatus;
        private string _warningMsg;
        private string _statusMsg;
        private bool _mIgnoreXyzChangeEvent = true;

        private double[] _overpressures;
        private double[] _impulses;
        private string _overpPlotFilepath;
        private string _impulsePlotFilepath;
        private float _massFlowRate;

        public UnconfinedOverpressureForm()
        {
            InitializeComponent();
        }

        private void UnconfinedOverpressureForm_Load(object sender, EventArgs e)
        {
            spinnerPictureBox.Hide();
            outputWarning.Hide();

            MethodSelector.DataSource = _state.OverpressureMethods;
            FlameSpeedSelector.DataSource = _state.MachFlameSpeeds;
            NozzleSelector.DataSource = _state.NozzleModels;
            PhaseSelector.DataSource = _state.Phases;

            _state.FuelTypeChangedEvent += OnFuelChange;

            _mIgnoreXyzChangeEvent = true;
            RefreshForm();
            // Initialize flex input
            ParseUtility.PutDoubleArrayIntoTextBox(LocXInput, _state.OverpressureX);
            ParseUtility.PutDoubleArrayIntoTextBox(LocYInput, _state.OverpressureY);
            ParseUtility.PutDoubleArrayIntoTextBox(LocZInput, _state.OverpressureZ);

            _mIgnoreXyzChangeEvent = false;
        }

        ~UnconfinedOverpressureForm()
        {
            _state.FuelTypeChangedEvent -= OnFuelChange;
        }

        private void OnFuelChange(object o, EventArgs e)
        {
            RefreshForm();
        }

        /// <summary>
        /// Change which parameters are displayed based on fuel selection
        /// </summary>
        private void RefreshForm()
        {
            var method = _state.SelectedOverpressureMethod;
            FlameSpeedSelector.Visible = method == _state.BstMethod;
            flameSpeedLabel.Visible = method == _state.BstMethod;

            MethodSelector.SelectedItem = _state.SelectedOverpressureMethod;
            FlameSpeedSelector.SelectedItem = _state.OverpressureFlameSpeed;
            NozzleSelector.DataSource = _state.NozzleModels;
            NozzleSelector.SelectedItem = _state.Nozzle;
            PhaseSelector.SelectedItem = _state.Phase;
            AutoSetLimits.Checked = _state.OverpAutoLimits;

            InputGrid.Rows.Clear();

            bool ignoreVal = _mIgnoreXyzChangeEvent;
            _mIgnoreXyzChangeEvent = true;
            // Create collection initially containing common params
            var formParams = ParameterInput.GetParameterInputList(new[]
            {
                _state.AmbientTemperature,
                _state.AmbientPressure,
                _state.OrificeDiameter,
                _state.ReleaseAngle,
                _state.OrificeDischargeCoefficient,
                _state.FluidPressure,
            });
            if (_state.DisplayTemperature())
            {
                formParams.Add(new ParameterInput(_state.FluidTemperature));
            }
            if (method == _state.TntMethod)
            {
                formParams.Add(new ParameterInput(_state.TntEquivalenceFactor));
            }

            if (!_state.OverpAutoLimits)
            {
                formParams.AddRange(ParameterInput.GetParameterInputList(new[]
                {
                    _state.OverpXMin, _state.OverpXMax,
                    _state.OverpYMin, _state.OverpYMax,
                    _state.OverpZMin, _state.OverpZMax,

                    _state.ImpulseXMin, _state.ImpulseXMax,
                    _state.ImpulseYMin, _state.ImpulseYMax,
                    _state.ImpulseZMin, _state.ImpulseZMax,
                }));
            }


            GridHelpers.InitParameterGrid(InputGrid, formParams, false);

            InputGrid.Columns[0].Width = 180;

            OverpContourInput.Text = "";
            ParseUtility.PutDoubleArrayIntoTextBox(OverpContourInput, _state.OverpressureContours);
            ImpulseContourInput.Text = "";
            ParseUtility.PutDoubleArrayIntoTextBox(ImpulseContourInput, _state.ImpulseContours);

            _mIgnoreXyzChangeEvent = ignoreVal;

            CheckFormValid();
        }

        private void CheckFormValid()
        {
            int alertType = 0;
            string alertText = "";

            if (_state.GetActiveFuel() == _state.FuelBlend && _state.SelectedOverpressureMethod == _state.BauwensMethod)
            {
                alertText = "Cannot assess blend with Bauwens calculation method";
                alertType = 2;
            }
            else if (_state.GetActiveFuel() != _state.FuelH2 || _state.Phase != _state.GasDefault)
            {
                alertText = "Unconfined overpressure analysis currently tailored to gaseous H2";
                alertType = 1;
            }

            if (!_state.ReleasePressureIsValid())
            {
                // if liquid, validate fuel pressure
                alertText = MessageContainer.ReleasePressureInvalid();
                alertType = 2;
            }

            if (_state.FuelFlowUnchoked())
            {
                MassFlowInput.Visible = true;
                massFlowLabel.Visible = true;
                MassFlowInput.Text = _state.FluidMassFlow.ToString();
            }
            else
            {
                MassFlowInput.Visible = false;
                massFlowLabel.Visible = false;
                MassFlowInput.Text = "";
            }

            // Verify x,y,z inputs
            if (LocYInput.Text.Trim().Length > 0)
            {
                var numXElems = CountElements(LocXInput.Text);
                var numYElems = CountElements(LocYInput.Text);
                var numZElems = CountElements(LocZInput.Text);
                if (!(numZElems == numYElems && numZElems == numXElems))
                {
                    alertText = "X, Y, Z location arrays must be the same size";
                    alertType = 2;
                }
                lblXElemCount.Text = numXElems + " elements";
                lblYElemCount.Text = numYElems + " elements";
                lblZElementCount.Text = numZElems + " elements";
            }

            inputWarning.Text = alertText;
            inputWarning.BackColor = _state.AlertBackColors[alertType];
            inputWarning.ForeColor = _state.AlertTextColors[alertType];
            inputWarning.Visible = (alertType != 0);
            SubmitBtn.Enabled = (alertType != 2);
        }

        private void Execute()
        {
            Trace.TraceInformation("Creating PhysicsInterface for python call");
            var physInt = new PhysicsInterface();
            _analysisStatus = physInt.AnalyzeUnconfinedOverpressure(out _statusMsg, out _warningMsg, out _overpressures, out _impulses,
                                                                    out _overpPlotFilepath, out _impulsePlotFilepath, out _massFlowRate);
            Trace.TraceInformation("PhysicsInterface call complete");
        }

        private void DisplayResults()
        {
            if (!_analysisStatus)
            {
                MessageBox.Show(_statusMsg);
                spinnerPictureBox.Hide();
                SubmitBtn.Enabled = true;
            }
            else if (_overpressures.Length == 0)
            {
                MessageBox.Show("Analysis yielded no data.");
                spinnerPictureBox.Hide();
                SubmitBtn.Enabled = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(_overpPlotFilepath))
                {
                    OverpPlotBox.Load(_overpPlotFilepath);
                }

                if (!string.IsNullOrEmpty(_impulsePlotFilepath))
                {
                    ImpulsePlotBox.Load(_impulsePlotFilepath);
                }

                OutputMassFlowRate.Text = _massFlowRate.ToString("E3");

                ResultGrid.Rows.Clear();
                for (var index = 0; index < _overpressures.Length; index++)
                {
                    double overp = _overpressures[index] / 1000;
                    double imp = _impulses[index] / 1000;
                    var values = new object[5];
                    values[0] = _state.OverpressureX[index].ToString("F4");
                    values[1] = _state.OverpressureY[index].ToString("F4");
                    values[2] = _state.OverpressureZ[index].ToString("F4");
                    values[3] = overp.ToString("E4");
                    values[4] = imp.ToString("E4");
                    ResultGrid.Rows.Add(values);
                }

                if (_warningMsg.Length != 0)
                {
                    outputWarning.Text = _warningMsg;
                    outputWarning.Show();
                }

                spinnerPictureBox.Hide();
                SubmitBtn.Enabled = true;
                outputTabControl.SelectTab(dataTab);
                mainTabControl.SelectTab(outputTab);

            }
        }

        public static double[] ExtractFloatArrayFromDelimitedString(string delimitedString, char delimiter)
        {
            char[] delimiters = { delimiter };
            var values = delimitedString.Split(delimiters);
            var result = new double[values.Length];
            for (var index = 0; index < result.Length; index++)
            {
                result[index] = double.NaN;

                if (double.TryParse(values[index], out double parsedValue)) result[index] = parsedValue;
            }

            return result;
        }

        private void ExtractAndSaveXyzValues()
        {
            if (!_mIgnoreXyzChangeEvent)
            {
                var xValues =
                    ExtractFloatArrayFromDelimitedString(LocXInput.Text, ',');
                var yValues =
                    ExtractFloatArrayFromDelimitedString(LocYInput.Text, ',');
                var zValues =
                    ExtractFloatArrayFromDelimitedString(LocZInput.Text, ',');
                if (xValues.Length == yValues.Length && yValues.Length == zValues.Length && zValues.Length > 0)
                {
                    _state.OverpressureX = xValues;
                    _state.OverpressureY = yValues;
                    _state.OverpressureZ = zValues;
                }
            }
            CheckFormValid();
        }

        private int CountElements(string textToParse)
        {
            var values = textToParse.Trim().Split(',');
            return values.Length;
        }

        private async void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (OverpPlotBox.Image != null)
            {
                OverpPlotBox.Image.Dispose();
                OverpPlotBox.Image = null;
            }
            ResultGrid.Rows.Clear();
            outputWarning.Hide();
            spinnerPictureBox.Show();
            SubmitBtn.Enabled = false;
            await Task.Run(() => Execute());
            DisplayResults();
        }

        private void GridInput_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            GridHelpers.ChangeParameterValue((DataGridView) sender, e, 1, 2);
            CheckFormValid();
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var sa = new EditableStringArray();
                string thisLine = null;

                foreach (DataGridViewColumn thisColumn in ResultGrid.Columns)
                    if (thisLine == null)
                        thisLine = thisColumn.HeaderText;
                    else
                        thisLine += "\t" + thisColumn.HeaderText;

                sa.Append(thisLine);

                foreach (DataGridViewRow thisRow in ResultGrid.Rows)
                {
                    thisLine = null;
                    foreach (DataGridViewCell thisCell in thisRow.Cells)
                        if (thisCell.Value != null)
                        {
                            var thisValue = thisCell.Value.ToString() ?? "";
                            if (thisLine == null)
                                thisLine = thisValue;
                            else
                                thisLine += "\t" + thisValue;
                        }

                    if (thisLine != null)
                    {
                        sa.Append(thisLine);
                        thisLine = null;
                    }
                }
                var clipboardText = sa.CombineToString(ArrayStringConversionOption.AppendCrlf);
                Clipboard.SetDataObject(clipboardText, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem copying to the clipboard: " + ex.Message);
            }
        }

        private void NozzleSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _state.Nozzle = (ModelPair)NozzleSelector.SelectedItem;
        }

        private void PhaseSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _state.Phase = (ModelPair)PhaseSelector.SelectedItem;
            RefreshForm();
        }

        private void MethodSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _state.SelectedOverpressureMethod = (ModelPair)MethodSelector.SelectedItem;
            RefreshForm();
        }

        private void Locations_TextChanged(object sender, EventArgs e)
        {
            ExtractAndSaveXyzValues();
        }

        private void FlameSpeedSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            double speed = (double)FlameSpeedSelector.SelectedItem;
            _state.OverpressureFlameSpeed = speed;
        }

        private void OverpContourInput_TextChanged(object sender, EventArgs e)
        {
            if (!_mIgnoreXyzChangeEvent)
            {
                var overpContours = new List<double>();

                string contourText = OverpContourInput.Text;
                Regex.Replace(contourText, @"\s+", "");  // trim whitespace
                if (contourText != "")
                {
                    overpContours = new List<double>(ExtractFloatArrayFromDelimitedString(OverpContourInput.Text, ','));
                }

                _state.OverpressureContours = overpContours.ToArray();
            }
        }

        private void ImpulseContourInput_TextChanged(object sender, EventArgs e)
        {
            if (!_mIgnoreXyzChangeEvent)
            {
                var impulseContours = new List<double>();
                string contourText = ImpulseContourInput.Text;
                Regex.Replace(contourText, @"\s+", "");  // trim whitespace
                if (contourText != "")
                {
                    impulseContours = new List<double>(ExtractFloatArrayFromDelimitedString(ImpulseContourInput.Text, ','));
                }

                _state.ImpulseContours = impulseContours.ToArray();
            }
        }

        private void AutoSetLimits_CheckedChanged(object sender, EventArgs e)
        {
            _state.OverpAutoLimits = AutoSetLimits.Checked;
            RefreshForm();
        }

        private void MassFlowInput_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(MassFlowInput.Text, out double result))
            {
                _state.FluidMassFlow = result;
            }
            else
            {
                MassFlowInput.Text = _state.FluidMassFlow.ToString();
            }
        }
    }
}