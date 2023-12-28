currentMatrix = $.transform
preRotate currentMatrix (eulertoquat (eulerAngles 90 0 0))
$.transform = currentMatrix


	