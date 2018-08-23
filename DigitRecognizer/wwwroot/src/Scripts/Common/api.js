import axios from 'axios';

function image() {
    function predict(image) {
        const file = new Blob([image.asBlob()], {type: 'image/png'});
        let form = new FormData();
        form.append('img', file, file.filename);
        return axios.put('image', form, {
            headers: {
                'Content-Type': `multipart/form-data;`,
            },
        })
    }

    return {
        predict: predict
    }
}

export const Image = image;

export default {
    Image: image
}