
let fMil : HTMLInputElement;
let fMoa : HTMLInputElement;
let fDecimals : HTMLInputElement;

let moaPerMil = 3.4377468;

let rawMil = 1.0;
let rawMoa = 1.0;

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

window.onload = () => {
	fMil = <HTMLInputElement>document.getElementById('fMil');
	fMoa = <HTMLInputElement>document.getElementById('fMoa');
	fDecimals = <HTMLInputElement>document.getElementById('fDecimals');

	fMil.addEventListener("change", (e: Event) => calculateMoa());
	fMoa.addEventListener("change", (e: Event) => calculateMil());
	fDecimals.addEventListener("change", (e: Event) => updateFields());

	calculateMoa(); //initialize the MOA field
}
