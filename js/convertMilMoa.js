//mil/moa conversion stuff
var fMil;
var fMoa;
var fDecimals;
var moaPerMil = 3.4377468;
var rawMil = 1.0;
var rawMoa = 1.0;
function calculateMoa() {
    rawMil = parseFloat(fMil.value);
    rawMoa = rawMil * moaPerMil;
    fMoa.value = rawMoa.toFixed(parseInt(fDecimals.value));
}
function calculateMil() {
    rawMoa = parseFloat(fMoa.value);
    rawMil = rawMoa / moaPerMil;
    fMil.value = rawMil.toFixed(parseInt(fDecimals.value));
}
function updateFields() {
    fMoa.value = rawMoa.toFixed(parseInt(fDecimals.value));
    fMil.value = rawMil.toFixed(parseInt(fDecimals.value));
}
//ranging calculator stuff
var fDistance;
var fAngle;
var fHeight;
var btnMetric;
var btnImperial;
var radioSolveFor;
var selDistanceUnit;
var selAngleUnit;
var selHeightUnit;
var solveFor;
function getRadioSelection(radio) {
    for (var i = 0; i < radioSolveFor.length; i++) {
        if (radio[i].checked) {
            return radio[i].id;
        }
    }
}
function updateSolveFor() {
    solveFor = getRadioSelection(radioSolveFor);
    if (solveFor === "distance") {
        fDistance.disabled = true;
        fAngle.disabled = false;
        fHeight.disabled = false;
    }
    else if (solveFor === "angle") {
        fDistance.disabled = false;
        fAngle.disabled = true;
        fHeight.disabled = false;
    }
    else if (solveFor === "height") {
        fDistance.disabled = false;
        fAngle.disabled = false;
        fHeight.disabled = true;
    }
}
function presetMetric() {
    selDistanceUnit.value = 'm';
    selAngleUnit.value = 'MIL';
    selHeightUnit.value = 'm';
}
function presetImperial() {
    selDistanceUnit.value = 'yd';
    selAngleUnit.value = 'MOA';
    selHeightUnit.value = 'ft';
}
//unit, how many meters that unit is, or meters/unit
//doing it the other way around actually results in numbers w/ longer decimals in the table
var distConversions = {
    "mm": 0.001,
    "cm": 0.01,
    "m": 1.0,
    "km": 1000.0,
    "in": 0.0254,
    "ft": 0.3048,
    "yd": 0.9144,
    "mi": 1609.344
};
//unit, mils/unit
var angConversions = {
    "MIL": 1.0,
    "MOA": 0.2908882087
};
function lengthUnitToMeter(length, unit) {
    return length * distConversions[unit];
}
function lengthMeterToUnit(length, unit) {
    return length / distConversions[unit];
}
function calculateFor() {
    if (solveFor === "distance") {
        if (fAngle.value !== "" && fHeight.value !== "") {
            var ang = parseFloat(fAngle.value) * angConversions[selAngleUnit.value]; //angle in rads
            var h = parseFloat(fHeight.value) * distConversions[selHeightUnit.value]; //height in meters
            fDistance.value = ((h / Math.tan(ang / 1000.0)) / distConversions[selDistanceUnit.value]).toString();
        }
    }
    else if (solveFor === "angle") {
        if (fDistance.value !== "" && fHeight.value !== "") {
            var d = parseFloat(fDistance.value) * distConversions[selDistanceUnit.value]; //distance in meters
            var h = parseFloat(fHeight.value) * distConversions[selHeightUnit.value]; //height in meters
            fAngle.value = ((Math.atan(h / d) * 1000.0) / angConversions[selAngleUnit.value]).toString();
        }
    }
    else if (solveFor === "height") {
        if (fDistance.value !== "" && fAngle.value !== "") {
            var d = parseFloat(fDistance.value) * distConversions[selDistanceUnit.value]; //distance in meters
            var ang = parseFloat(fAngle.value) * angConversions[selAngleUnit.value]; //angle in rads
            fHeight.value = ((d * Math.tan(ang / 1000.0)) / distConversions[selHeightUnit.value]).toString();
        }
    }
}
window.onload = function () {
    //convert mil/moa stuff
    fMil = document.getElementById('fMil');
    fMoa = document.getElementById('fMoa');
    fDecimals = document.getElementById('fDecimals');
    fMil.addEventListener("change", function (e) { return calculateMoa(); });
    fMoa.addEventListener("change", function (e) { return calculateMil(); });
    fDecimals.addEventListener("change", function (e) { return updateFields(); });
    calculateMoa(); //initialize the MOA field
    //ranger calculator stuff
    fDistance = document.getElementById('fDistance');
    fAngle = document.getElementById('fAngle');
    fHeight = document.getElementById('fHeight');
    btnMetric = document.getElementById('btnMetric');
    btnImperial = document.getElementById('btnImperial');
    radioSolveFor = document.getElementsByName('radioSolveFor');
    selDistanceUnit = document.getElementById('selDistanceUnit');
    selAngleUnit = document.getElementById('selAngleUnit');
    selHeightUnit = document.getElementById('selHeightUnit');
    btnMetric.addEventListener("click", function (e) { return presetMetric(); });
    btnImperial.addEventListener("click", function (e) { return presetImperial(); });
    for (var i = 0; i < radioSolveFor.length; i++) {
        radioSolveFor[i].onchange = updateSolveFor;
    }
    fDistance.addEventListener("change", function (e) { return calculateFor(); });
    fAngle.addEventListener("change", function (e) { return calculateFor(); });
    fHeight.addEventListener("change", function (e) { return calculateFor(); });
    selDistanceUnit.onchange = calculateFor;
    selAngleUnit.onchange = calculateFor;
    selHeightUnit.onchange = calculateFor;
    updateSolveFor();
};
//# sourceMappingURL=convertMilMoa.js.map