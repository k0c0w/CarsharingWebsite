const EMAIL_REGEXP = /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;


function commonFromFieldsValidator(form) {
    deleteErrors(form);
    const inputs = getFormInputs(form);
    const requiredFields = inputs.filter(x => x.required);
    
    if(requiredFields.some(x => x.value.length < 1)) {
        insertFieldRequiredNode(requiredFields);
        return false;
    }
    return true;
}

function isValidAge(age) {
    const value = age.value;
    if(!Number.isInteger(parseFloat(value))) {
        appendError(age, genereteErrorNode("Количество полных лет."));
        return false;
    }
    if(parseInt(value) < 23) {
        appendError(age, genereteErrorNode("Вам должно быть 23 и более."))
        return false;
    }
    return true;
}

function checkPasswords(password, passwordRep) {
    //todo: проверить пароль на дефолт символы
    if(password.value != passwordRep.value){
        appendError(passwordRep, genereteErrorNode("Пароли не совпадают."));
        return false;
    }
    return true;
}

function checkEmail(email) {
    if(!EMAIL_REGEXP.test(email.value))
    {
        appendError(email, genereteErrorNode("Неверный формат почты."));
        return false;
    }
    return true;
}

function checkNameAndSurname(name, surname) {
    const errorMessage = "Не поддерживаемый формат.";
    const empties = /\s+/;
    const special_symbols = /[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~0-9]/;
    name.value = name.value.replace(empties, ' ');
    surname.value = surname.value.replace(empties, ' ');
    const is_incorrect_name = special_symbols.test(name.value);
    const is_incorrect_surname = special_symbols.test(surname.value);
    if(is_incorrect_name)
        appendError(name, genereteErrorNode(errorMessage));
    if(is_incorrect_surname)
        appendError(surname, genereteErrorNode(errorMessage));
    return !(is_incorrect_name || is_incorrect_surname) 
}

const deleteErrors = (form) => form.querySelectorAll('.form-error').forEach(x => { 
    let inputClassList = x.parentElement.childNodes[0].classList;
    if(inputClassList.contains("error"))
        inputClassList.remove("error");
    x.remove();
});


function getFormInputs(form) {
    const inputs = [];
    form.querySelectorAll('.form-input').forEach(x => inputs.push(x));
    trimAllValues(inputs);
    return inputs;
}

function insertFieldRequiredNode(requiredInputs) {
    requiredInputs.forEach(input => {
        if(input.value.length < 1) {
            appendError(input, genereteErrorNode("Поле не должно быть пустым."));
        }
    })
}

function appendError(input, error) {
    input.parentElement.appendChild(error);
    if(!input.classList.contains("error"))
        input.classList.add("error");
}

function genereteErrorNode(text) {
    const error = document.createElement('p');
    error.className = 'form-error';
    error.innerHTML = text;
    return error;
}

function trimAllValues(inputsCollection) {
    for (let i = 0; i < inputsCollection.length; i++) {
        const value = inputsCollection[i].value;
        if(value.length > 0)
            inputsCollection[i].value = value.trimStart().trimEnd();
    }
}

export function areValidRegistrationFields(form) {
    try {
        if(!commonFromFieldsValidator(form)) return false;

        const result = checkEmail(form.querySelector("#email")) & isValidAge(form.querySelector("#age")) 
        & checkPasswords(form.querySelector("#password"), form.querySelector("#password_repeat"))
        & checkNameAndSurname(form.querySelector("#name"), form.querySelector("#surname")); 

        const check_box = form.querySelector("#data-processing-agreement");
        if(!check_box.checked){
            const error = genereteErrorNode("Вы должны дать согласие на обработку персональных данных.");
            check_box.parentElement.appendChild(error);
            return false;
        }

        return result;    
    }
    catch(exception){
        console.log(exception);
        alert("Что-то пошло не так. Попробуйте перезагрузить страницу.");
        return false;
    }
}    


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