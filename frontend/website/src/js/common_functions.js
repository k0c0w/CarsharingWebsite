export function getVMErrors(errors)
{
    const state = {};
    for(const[key, value] of Object.entries(errors)){
        state[key.toLowerCase()] = value[0] ?? "";
    }
    return state;
}
