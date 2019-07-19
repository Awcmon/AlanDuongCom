/*
window.onload = () => {
	var mil = document.getElementById('fMil');
	mil.addEventListener("onchange", (e) => test());
	console.log("test");
};
*/

window.onload = function () {
	var fMil = document.getElementById('fMil');
	var fMoa = document.getElementById('fMoa');
	var fDecimals = document.getElementById('fDecimals');

	var moaPerMil = 3.4377468;

	fMil.addEventListener("change", function () {
		fMoa.value = (parseFloat(fMil.value) * moaPerMil).toFixed(parseInt(fDecimals.value));
	});

	fMoa.addEventListener("change", function () {
		fMil.value = (parseFloat(fMoa.value) / moaPerMil).toFixed(parseInt(fDecimals.value));
	});

	//initialize the moa field
	fMoa.value = (parseFloat(fMil.value) * moaPerMil).toFixed(parseInt(fDecimals.value));
};
