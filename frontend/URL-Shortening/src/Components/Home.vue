<template>
  <v-app>
    <!-- App Bar -->
    <v-app-bar class="custom-app-bar">
      <v-app-bar-title class="text-center">
        URL Shortener
      </v-app-bar-title>
    </v-app-bar>

    <!-- Snackbar Component -->
    <v-snackbar
      v-model="messageSB.show"
      :color="messageSB.color"
      :timeout="messageSB.timeout"
      location="bottom"
    >
      {{ messageSB.text }}
    </v-snackbar>

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

          <!-- Short Code Input -->
          <v-text-field 
            clearable 
            variant="solo-filled"
            color="primary"
            class="mt-8"
            backgroundColor="white"
            label="Enter a short code (10 letters or digits)..."
            v-model="shortCode" 
            placeholder="abc123def4" 
            @keyup.enter="checkShortCode"
          />

          <v-btn @click="checkShortCode" class="custom-btn">Search</v-btn>
        
        </v-col>
      </v-row>
    </v-main>
  </v-app>
</template>

<script>
  import { ref } from 'vue'
  import axios from 'axios'
  import { useRouter } from 'vue-router'

  export default {
    setup() {
      const longURL = ref('')
      const shortURL = ref('')
      const shortCode = ref('')

      const messageSB = ref({
        show: false,
        text: '',
        color: 'error',
        timeout: 3000
      })

      const router = useRouter()

      const API_BASE_URL = import.meta.env.VITE_API_URL
      
      // shortens the longURL and sets the shortURL value
      async function shortenUrl() {
        try {
          const response = await axios.post('/api/shorturl', {url: longURL.value});

          shortURL.value = `${API_BASE_URL}/${response.data.shortCode}`
          showResult('URL shortened successfully!', 'success')
        }
        catch (error) {
          console.error('Error Shortening Url: ', error)
          showResult('Invalid URL! Make sure the input is a proper URL.')
        }
      }

      // searches for the shortcode and takes the user to the shortcode component if it exists
      async function checkShortCode() {
        const temp = shortCode.value.toLowerCase().trim()
        if (temp.length !== 10 || !/^[a-zA-Z0-9]+$/.test(temp)) {
          showResult('Invalid code! Make sure the code is 10 letters or digits.')
          return
        }

        try {
          const reponse = await axios.get(`/api/shorturl/${temp}`)

          await router.push({
          name: 'Shortcode',
          params: { code: temp }
          })
        } catch (error) {
          console.error('Error: ', error)

          if (error.response?.status === 404) {
            showResult('404 shortcode not found.')
          } else {
            showResult('Error searching for shortcode.')
          }
        }
      }

      function showResult(message, type = 'error') {
        messageSB.value = {
          show: true,
          text: message,
          color: type,
          setTimeout: 3000
        }
      }

      return {
        longURL,
        shortURL,
        shortCode,
        messageSB,
        shortenUrl,
        checkShortCode
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