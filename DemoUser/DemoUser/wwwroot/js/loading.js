function Loading(options, settime) {
	var loadElement = document.querySelector(options.load);
	var formElement = document.querySelector(options.form);

	setTimeout(function () {
		loadElement.style.display = "none";
		formElement.style.display = "block";
	}, settime);
}