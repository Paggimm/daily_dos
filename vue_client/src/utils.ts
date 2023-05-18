export async function fetchWithTimeout(
    resource: RequestInfo,
    options: RequestInit,
    timeout: number
): Promise<Response> {
    const controller = new AbortController()
    const id = setTimeout(() => controller.abort(), timeout)

    const response = await fetch(resource, {
        ...options,
        signal: controller.signal,
    })
    clearTimeout(id)

    return response
}

/**
 * wrapper method to send a default request
 * TODO: own method for GET without body
 * @param uri endpoint without base-url
 * @param body json-encoded string or FormData or null
 * @param httpMethod GET|POST|PATCH|DELETE
 * @param token authorization-token
 */
export async function fetchRequest(uri: string, body: string | FormData | undefined, httpMethod: string, token?: string): Promise<Response> {
    const headers = new Headers()
    headers.append("pragma", "no-cache")
    headers.append("cache-control", "no-cache")
    headers.append("Authorization", `Bearer ${token}`)

    const requestInit: RequestInit = {
        method: httpMethod,
        headers: headers,
        body,
    }

    return fetchWithTimeout(getBaseUrl() + uri, requestInit, 15000)
}

function getBaseUrl(): string {
    if (process.env.NODE_ENV === "development") {
        return "http://localhost:8085/";
    } else {
        return "http://api.dailydos/"
    }
}
