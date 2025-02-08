export async function setMessage() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports("SharedComponents.dll");
    alert(exports.SharedComponents.Demo.JSInteropSamples.JSToStaticNET.GetMessageFromNET());
}

export async function showMessage() {
    await setMessage();
}