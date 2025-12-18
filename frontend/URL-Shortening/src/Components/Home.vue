<template>
  <v-app>
    <!-- App Bar -->
    <v-app-bar class="custom-app-bar">
      <v-app-bar-title class="text-center">
        URL Shortener
      </v-app-bar-title>
    </v-app-bar>

    <!-- Main Body -->
    <v-main class="mt-8">
      <v-row class="justify-center">
        <v-col cols="12" sm="10" md="8" lg="6">
          <!-- Long URL Input -->
          <v-text-field 
            clearable 
            variant="solo-filled"
            color="primary"
            backgroundColor="white"
            label="Enter a url..."
            v-model="longURL" 
            placeholder="https://www.example.com" 
            @keyup.enter="shortenUrl"
          />

          <!-- Shorten button and shortURL display -->
          <div class="d-flex align-center justify-space-between">
          <v-btn @click="shortenUrl" class="custom-btn">Shorten</v-btn>
          <v-card variant="default" :text="shortURL" style="font-size: 25px"/>
          </div>
        
        </v-col>
      </v-row>
    </v-main>
  </v-app>
</template>

<script>
  import { ref } from 'vue'
  import axios from 'axios'
  import { useRoute } from 'vue-router'

  export default {
    setup() {
      const longURL = ref('')
      const shortURL = ref('')

      const API_BASE_URL = import.meta.env.VITE_API_URL
      
      // shortens the longURL and sets the shortURL value
      async function shortenUrl() {
        try {
          const response = await axios.post('/api/shorturl', {url: longURL.value});

          shortURL.value = `${API_BASE_URL}/${response.data.shortCode}`
        }
        catch (error) {
          console.error('Error Shortening Url: ', error)
        }
      }

      return {
        longURL,
        shortURL,
        shortenUrl
      }
    }
  }
</script>

<style>
  .custom-btn {
    background-color: #d3d3d3 !important;
    border-radius: 24px !important;
  }
</style>