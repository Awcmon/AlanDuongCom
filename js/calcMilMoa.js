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
window.onload = function () {
    fMil = document.getElementById('fMil');
    fMoa = document.getElementById('fMoa');
    fDecimals = document.getElementById('fDecimals');
    fMil.addEventListener("change", function (e) { return calculateMoa(); });
    fMoa.addEventListener("change", function (e) { return calculateMil(); });
    fDecimals.addEventListener("change", function (e) { return updateFields(); });
    calculateMoa(); //initialize the MOA field
};
//# sourceMappingURL=calcMilMoa.js.map