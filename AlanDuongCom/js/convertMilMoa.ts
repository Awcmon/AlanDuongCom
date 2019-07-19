
//mil/moa conversion stuff
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

//ranging calculator stuff
let fDistance: HTMLInputElement;
let fAngle: HTMLInputElement;
let fHeight: HTMLInputElement;

let btnMetricRanging: HTMLButtonElement;
let btnImperialRanging: HTMLButtonElement;
let btnImperialMilRanging: HTMLButtonElement;
let btnImperialGroupMOA: HTMLButtonElement;

let radioSolveFor: NodeListOf<HTMLInputElement>;

let selDistanceUnit: HTMLSelectElement;
let selAngleUnit: HTMLSelectElement;
let selHeightUnit: HTMLSelectElement;

let solveFor: string;

function getRadioSelection(radio: NodeListOf<HTMLInputElement>) : string
{
	for (let i = 0; i < radioSolveFor.length; i++) {
		if (radio[i].checked) {
			return radio[i].id;
		}
	}
}

function updateSolveFor() : any {
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

function setPreset(distUnit:string, angleUnit:string, heightUnit:string, solveForId:number) {
	selDistanceUnit.value = distUnit;
	selAngleUnit.value = angleUnit;
	selHeightUnit.value = heightUnit;
	radioSolveFor[solveForId].click();
	fDistance.value = "";
	fAngle.value = "";
	fHeight.value = "";
	calculateFor();
}

//unit, how many meters that unit is, or meters/unit
//doing it the other way around actually results in numbers w/ longer decimals in the table
const distConversions: Record<string, number> = {
	"mm": 0.001,
	"cm": 0.01,
	"m": 1.0,
	"km": 1000.0,
	"in": 0.0254,
	"ft": 0.3048,
	"yd": 0.9144,
	"mi": 1609.344
}

//unit, mils/unit
const angConversions: Record<string, number> = {
	"MIL": 1.0,
	"MOA": 0.2908882087
}


function lengthUnitToMeter(length: number, unit: string): number {
	return length * distConversions[unit];
}

function lengthMeterToUnit(length: number, unit: string): number {
	return length / distConversions[unit];
}

function calculateFor() {
	if (solveFor === "distance") {
		if (fAngle.value !== "" && fHeight.value !== "") {
			let ang = parseFloat(fAngle.value) * angConversions[selAngleUnit.value]; //angle in rads
			let h = parseFloat(fHeight.value) * distConversions[selHeightUnit.value]; //height in meters
			fDistance.value = ((h / Math.tan(ang / 1000.0)) / distConversions[selDistanceUnit.value]).toString();
		}
	}
	else if (solveFor === "angle") {
		if (fDistance.value !== "" && fHeight.value !== "") {
			let d = parseFloat(fDistance.value) * distConversions[selDistanceUnit.value]; //distance in meters
			let h = parseFloat(fHeight.value) * distConversions[selHeightUnit.value]; //height in meters
			fAngle.value = ((Math.atan(h / d) * 1000.0) / angConversions[selAngleUnit.value]).toString();
		}
	}
	else if (solveFor === "height") {
		if (fDistance.value !== "" && fAngle.value !== "") {
			let d = parseFloat(fDistance.value) * distConversions[selDistanceUnit.value]; //distance in meters
			let ang = parseFloat(fAngle.value) * angConversions[selAngleUnit.value]; //angle in rads
			fHeight.value = ((d * Math.tan(ang / 1000.0)) / distConversions[selHeightUnit.value]).toString();
		}
	}
}

//TODO: round to decimal place
//TODO: more presets
//TODO: clear field on enter?
window.onload = () => {
	//convert mil/moa stuff
	fMil = <HTMLInputElement>document.getElementById('fMil');
	fMoa = <HTMLInputElement>document.getElementById('fMoa');
	fDecimals = <HTMLInputElement>document.getElementById('fDecimals');

	fMil.addEventListener("input", (e: Event) => calculateMoa());
	fMoa.addEventListener("input", (e: Event) => calculateMil());
	fDecimals.addEventListener("change", (e: Event) => updateFields());
	fMil.addEventListener("focus", function () { this.select() });
	fMoa.addEventListener("focus", function () { this.select() });

	calculateMoa(); //initialize the MOA field

	//ranger calculator stuff
	fDistance = <HTMLInputElement>document.getElementById('fDistance');
	fAngle = <HTMLInputElement>document.getElementById('fAngle');
	fHeight = <HTMLInputElement>document.getElementById('fHeight');

	btnMetricRanging = <HTMLButtonElement>document.getElementById('btnMetricRanging');
	btnImperialRanging = <HTMLButtonElement>document.getElementById('btnImperialRanging');
	btnImperialMilRanging = <HTMLButtonElement>document.getElementById('btnImperialMilRanging');
	btnImperialGroupMOA = <HTMLButtonElement>document.getElementById('btnImperialGroupMOA');

	radioSolveFor = <NodeListOf<HTMLInputElement>>document.getElementsByName('radioSolveFor');

	selDistanceUnit = <HTMLSelectElement>document.getElementById('selDistanceUnit');
	selAngleUnit = <HTMLSelectElement>document.getElementById('selAngleUnit');
	selHeightUnit = <HTMLSelectElement>document.getElementById('selHeightUnit');

	//add action listeners
	btnMetricRanging.addEventListener("click", (e: Event) => setPreset('m', 'MIL', 'm', 0));
	btnImperialRanging.addEventListener("click", (e: Event) => setPreset('yd', 'MOA', 'ft', 0));
	btnImperialMilRanging.addEventListener("click", (e: Event) => setPreset('yd', 'MIL', 'ft', 0));
	btnImperialGroupMOA.addEventListener("click", (e: Event) => setPreset('yd', 'MOA', 'in', 1));

	for (let i = 0; i < radioSolveFor.length; i++) {
		radioSolveFor[i].onchange = updateSolveFor; 
	}

	fDistance.addEventListener("input", (e: Event) => calculateFor());
	fAngle.addEventListener("input", (e: Event) => calculateFor());
	fHeight.addEventListener("input", (e: Event) => calculateFor());

	//select fields on enter
	fDistance.addEventListener("focus", function () { this.select() });
	fAngle.addEventListener("focus", function () { this.select() });
	fHeight.addEventListener("focus", function () { this.select() });

	selDistanceUnit.onchange = calculateFor;
	selAngleUnit.onchange = calculateFor;
	selHeightUnit.onchange = calculateFor;

	updateSolveFor();
}
