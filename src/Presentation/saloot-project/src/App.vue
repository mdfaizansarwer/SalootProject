<template>
  <div id="app">
    <notifications position="bottom right" />
    <router-view />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";


@Component({
  components: {}
})

export default class App extends Vue { }
</script>