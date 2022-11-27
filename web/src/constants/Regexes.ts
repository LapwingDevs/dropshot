export const passwordRegex = new RegExp('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$');
export const onlyLetters = new RegExp(/^([A-Za-z\u00C0-\u00D6\u00D8-\u00f6\u00f8-\u00ff\s\'\-]*)$/gi);
