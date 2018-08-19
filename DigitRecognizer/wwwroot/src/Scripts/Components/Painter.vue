<template>
    <div class="d-flex flex-column align-items-center mt-3"
         v-loading="true"
    >
        <div class="painterro"
             id="painterro"
        ></div>
        <b-button v-if="!loadingPrediction"
                  color="primary"
                  type="button"
                  class="mt-5 btn btn-primary"
                  @click="saveImage"
                  data-loading-text="<i class='fa fa-spinner fa-spin '></i> Processing Order"
        >
            Predict
        </b-button>
        <div v-else class="mt-5">
            Computing
        </div>
        <b-modal title="Predict result"
                 ok-only
                 v-model="showDialog"
        >
            <p>Predicted number: {{predictedNumber}}</p>
            <p>Predicted percent: {{predictedPercent}} %</p>
        </b-modal>
    </div>
</template>

<script>
    import Painterro from 'painterro';
    import axios from 'axios';

    export default {
        name: "MPAINTERRO",

        mounted() {
            this.showPainerro();
        },
        data() {
            return {
                Painterro: null,
                showDialog: false,
                loadingPrediction: false,
                predictedNumber: 0,
                predictedPercent: 0
            }
        },
        methods: {
            showPainerro() {
                let _this = this;
                this.Painterro = Painterro({
                    id: 'painterro',
                    activeColor: '#00ff00',
                    defaultTool: 'brush',
                    hiddenTools: ['crop', 'line', 'rect', 'ellipse', 'text', 'rotate', 'resize', 'save', 'open', 'close', 'settings'],
                    saveHandler: function (image, done) {
                        _this.loadingPrediction = true;
                        const file = new Blob([image.asBlob()], {type: 'image/png'});
                        let form = new FormData();
                        form.append('img', file, file.filename);
                        axios.put('image', form, {
                            headers: {
                                'Content-Type': `multipart/form-data; boundary=${image._boundary}`,
                            },
                        }).then((response) => {
                            let data = response.data;
                            _this.predictedNumber = data.number;
                            _this.predictedPercent = data.probability;
                            done();
                            _this.showDialog = true;
                        }).catch(() => {
                            done();
                        }).then(() => {
                            _this.loadingPrediction = false;
                        });
                    }
                });
                this.Painterro.show();
            },
            saveImage() {
                this.Painterro.save();
            }
        }
    }
</script>

<style lang="scss" src="@/Styles/Components/Painter/painter.scss"></style>h