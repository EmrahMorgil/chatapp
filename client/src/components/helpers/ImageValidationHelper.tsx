const ImageValidationHelper = (image: File)=>{
    const allowedExtensions = ["jpg", "jpeg", "png", "gif"];
    const fileExtension = image.name.split(".").pop()?.toLowerCase() ?? "";

    if (allowedExtensions.includes(fileExtension)) return true;
    else return false;
}

export default ImageValidationHelper;