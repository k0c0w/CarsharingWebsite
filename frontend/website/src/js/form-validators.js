const EMAIL_REGEXP = /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;


export function areValidLoginFields(login, password, setErrors) {
    const errors = {};
    login.value = login.value.trimEnd().trimStart();
    login = login.value;
    if(!EMAIL_REGEXP.test(login))
        errors.login = "Используйте зарегестрированную почту для входа.";
    password.value = password.value.trimEnd().trimStart();
    password = password.value;
    if(password.length < 1)
        errors.password = "Поле не должно быть пустым.";
    setErrors(errors);
    return !(errors.login || errors.password);
}