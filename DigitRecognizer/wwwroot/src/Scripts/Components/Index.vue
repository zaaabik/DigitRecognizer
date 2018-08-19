<template>
    <div class="core">
        <section class="header">
            <div class="header__navigation row">
                <div class="header__links col-10">
                    <router-link to="/">
                        Digit recognizer
                    </router-link>
                </div>
                <div class="header__info">
                </div>
            </div>
        </section>
        <router-view>
        </router-view>
    </div>
</template>

<script>
    export default {
        name: "Home"
    }
</script>


<style lang="scss" src="@/Styles/Core/header.scss"></style>
<style lang="scss" src="@/Styles/Core/core.scss"></style>