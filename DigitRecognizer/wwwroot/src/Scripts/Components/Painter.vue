<template>
    <div class="d-flex flex-column align-items-center mt-3"
    >
        <div class="painterro"
             id="painterro"
             :class="isMobile"
        ></div>
        <b-button v-if="!loadingPrediction"
                  color="primary"
                  :class="isMobile"
                  type="button"
                  class="mt-5 btn btn-primary predict-button"
                  @click="saveImage"
        >
            Predict
        </b-button>
        <div v-else class="mt-5">
            Computing
        </div>
        <b-modal title="Predict result"
                 ok-only
                 v-model="showDialog"
                 class="predict-modal align-items-center"
                 :class="isMobile"
        >
            <p>Predicted number: {{predictedNumber}}</p>
            <p>Predicted percent: {{predictedPercent}} %</p>
        </b-modal>
    </div>
</template>
<script>
    import Painterro from 'painterro';
    import {Image} from '@/Scripts/Common/api.js'

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
                    activeColor: '#000000',
                    defaultTool: 'brush',
                    availableLineWidths: [10],
                    defaultLineWidth: 10,
                    fixMobilePageReloader: false,
                    hiddenTools: ['crop', 'toolbar', 'weight', 'line', 'rect', 'ellipse', 'text', 'rotate', 'resize', 'save', 'open', 'close', 'settings'],
                    saveHandler: function (image, done) {
                        _this.loadingPrediction = true;
                        Image().predict(image).then((response) => {
                            let data = response.data;
                            _this.predictedNumber = data.number;
                            _this.predictedPercent = Math.round(data.probability * 100) / 100;
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
        },
        computed: {
            isMobile() {
                return /Android|webOS|iPhone|iPad|iPod|BlackBerry|BB|PlayBook|IEMobile|Windows Phone|Kindle|Silk|Opera Mini/i.test(
                    navigator.userAgent) ? 'mobile' : '';

            }
        }

    }
</script>

<style lang="scss" src="@/Styles/Components/Painter/painter.scss"></style>h